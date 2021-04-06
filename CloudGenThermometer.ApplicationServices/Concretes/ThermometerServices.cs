using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudGenThermometer.ReadModel.Abstracts;
using CloudGenThermometer.ReadModel.Dtos;
using CloudGenThermometer.Shared.Abstracts;
using CloudGenThermometer.Shared.CustomTypes;
using CloudGenThermometer.Shared.JsonModel;
using CloudGenThermometer.Shared.Services;
using Microsoft.Extensions.Logging;

namespace CloudGenThermometer.ApplicationServices.Concretes
{
    public sealed class ThermometerServices : BaseService, IThermometerServices
    {
        public ThermometerServices(IPersister persister, ILoggerFactory loggerFactory) : base(persister, loggerFactory)
        {
        }

        public async Task AppendValuesAsync(DeviceId deviceId, DeviceName deviceName, Temperature temperature,
            CommunicationDate communicationDate)
        {
            try
            {
                var trendDto = new ThermometerTrend(deviceId, deviceName, communicationDate, temperature);
                await this.Persister.InsertAsync(trendDto);
            }
            catch (Exception ex)
            {
                this.Logger.LogError(CommonServices.GetDefaultErrorTrace(ex));
                throw;
            }
        }

        public async Task<IEnumerable<ThermometerTrendJson>> GetThermometerTrendAsync(DeviceId deviceId)
        {
            try
            {
                var trendDto =
                    await this.Persister.FindAsync<ThermometerTrend>(t => t.DeviceId.Equals(deviceId.ToString()));
                var trendArray = trendDto as ThermometerTrend[] ?? trendDto.ToArray();

                return trendArray.Any()
                    ? trendArray.Select(dto => dto.ToJson())
                    : Enumerable.Empty<ThermometerTrendJson>();
            }
            catch (Exception ex)
            {
                this.Logger.LogError(CommonServices.GetDefaultErrorTrace(ex));
                throw;
            }
        }
    }
}