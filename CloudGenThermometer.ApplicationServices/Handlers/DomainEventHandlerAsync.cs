using System;
using System.Threading;
using System.Threading.Tasks;
using FourSolid.Athena.Messages.Events;
using Microsoft.Extensions.Logging;

namespace CloudGenThermometer.ApplicationServices.Handlers
{
    public abstract class DomainEventHandlerAsync<T> : IDomainEventHandlerAsync<T> where T : class, IDomainEvent
    {
        protected readonly ILogger Logger;

        protected DomainEventHandlerAsync(ILoggerFactory loggerFactory)
        {
            this.Logger = loggerFactory.CreateLogger(this.GetType());
        }

        public abstract Task HandleAsync(T @event, CancellationToken cancellationToken = new CancellationToken());

        #region Dispose
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~DomainEventHandlerAsync()
        {
            this.Dispose(false);
        }
        #endregion
    }
}