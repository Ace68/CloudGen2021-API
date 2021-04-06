using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CloudGenThermometer.ReadModel.Abstracts;
using FourSolid.Common.Services;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace CloudGenThermometer.ReadModel.MongoDb.Repository
{
    public sealed class Persister : IPersister
    {
        private readonly IMongoDatabase _mongoDatabase;
        private readonly ILogger _logger;

        public Persister(IMongoDatabase mongoDatabase, ILoggerFactory loggerFactory)
        {
            this._mongoDatabase = mongoDatabase;
            this._logger = loggerFactory.CreateLogger(this.GetType());
        }

        public async Task<T> GetByIdAsync<T>(string id) where T : DtoBase
        {
            try
            {
                var collection = this._mongoDatabase.GetCollection<T>(GetCollectionName<T>()).AsQueryable();

                var results = await Task.Run(() => collection.Where(t => t.Id.Equals(id)));
                return await results.AnyAsync()
                    ? results.First()
                    : null;
            }
            catch (Exception ex)
            {
                this._logger.LogError(CommonServices.GetDefaultErrorTrace(ex));
                throw;
            }
        }

        public async Task InsertAsync<T>(T dtoToInsert) where T : DtoBase
        {
            try
            {
                var collection = this._mongoDatabase.GetCollection<T>(GetCollectionName<T>());
                await collection.InsertOneAsync(dtoToInsert);
            }
            catch (Exception ex)
            {
                this._logger.LogError(CommonServices.GetDefaultErrorTrace(ex));
                throw;
            }
        }

        public async Task ReplaceAsync<T>(T dtoToUpdate) where T : DtoBase
        {
            try
            {
                var collection = this._mongoDatabase.GetCollection<T>(GetCollectionName<T>());
                await collection.ReplaceOneAsync(x => x.Id == dtoToUpdate.Id, dtoToUpdate);
            }
            catch (Exception ex)
            {
                this._logger.LogError(CommonServices.GetDefaultErrorTrace(ex));
                throw;
            }
        }

        public async Task UpdateOneAsync<T>(string id, Dictionary<string, object> propertiesToUpdate) where T : DtoBase
        {
            try
            {
                var collection = this._mongoDatabase.GetCollection<T>(GetCollectionName<T>());

                var updateDefination = propertiesToUpdate
                    .Select(dataField => Builders<T>.Update.Set(dataField.Key, dataField.Value)).ToList();
                var combinedUpdate = Builders<T>.Update.Combine(updateDefination);

                var updateResult = await collection.UpdateOneAsync(
                    Builders<T>.Filter.Eq("_id", id),
                    combinedUpdate);

                if (!updateResult.IsAcknowledged)
                    throw new Exception($"Failed to Update {typeof(T).Name}");
            }
            catch (Exception ex)
            {
                this._logger.LogError(CommonServices.GetDefaultErrorTrace(ex));
                throw;
            }
        }

        public async Task UpdateManyAsync<T, TField>(Expression<Func<T, bool>> filter,
            Expression<Func<T, TField>> field, TField value) where TField : FieldDefinition<T, TField> where T : DtoBase
        {
            try
            {
                var collection = this._mongoDatabase.GetCollection<T>(GetCollectionName<T>());
                await collection.UpdateManyAsync(filter, Builders<T>.Update.Set(field, value), new UpdateOptions
                    {IsUpsert = false});
            }
            catch (Exception ex)
            {
                this._logger.LogError(CommonServices.GetDefaultErrorTrace(ex));
                throw;
            }
        }

        public async Task DeleteAsync<T>(string id) where T : DtoBase
        {
            try
            {
                var collection = this._mongoDatabase.GetCollection<T>(GetCollectionName<T>());
                var filter = Builders<T>.Filter.Eq("_id", id);
                await collection.FindOneAndDeleteAsync(filter);
            }
            catch (Exception ex)
            {
                this._logger.LogError(CommonServices.GetDefaultErrorTrace(ex));
                throw;
            }
        }

        public async Task DeleteManyAsync<T>(Expression<Func<T, bool>> filter) where T : DtoBase
        {
            try
            {
                var collection = this._mongoDatabase.GetCollection<T>(GetCollectionName<T>());
                await collection.DeleteManyAsync(filter);
            }
            catch (Exception ex)
            {
                this._logger.LogError(CommonServices.GetDefaultErrorTrace(ex));
                throw;
            }
        }

        public async Task<IEnumerable<T>> FindAsync<T>(Expression<Func<T, bool>> filter = null) where T : DtoBase
        {
            try
            {
                var collection = this._mongoDatabase.GetCollection<T>(GetCollectionName<T>()).AsQueryable();

                return await Task.Run(() => filter != null
                    ? collection.Where(filter)
                    : collection);
            }
            catch (Exception ex)
            {
                this._logger.LogError(CommonServices.GetDefaultErrorTrace(ex));
                throw;
            }
        }

        private static string GetCollectionName<T>() where T : DtoBase
        {
            //var typeName = typeof(T).Name.ToLower();
            //typeName = typeName.Replace("dto", "");

            //return typeName.EndsWith("s")
            //    ? typeName + "es"
            //    : typeName + "s";

            return typeof(T).Name;
        }
    }
}