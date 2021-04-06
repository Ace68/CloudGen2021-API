using System.Threading;
using System.Threading.Tasks;
using CloudGenThermometer.Messages.Events;
using CloudGenThermometer.Shared.Abstracts;
using Microsoft.Extensions.Logging;

namespace CloudGenThermometer.ApplicationServices.Handlers
{
    public sealed class ThermometerValuesUpdatedEventHandler : DomainEventHandlerAsync<ThermometerValuesUpdated>
    {
        private readonly IThermometerServices _thermometerServices;
        
        public ThermometerValuesUpdatedEventHandler(ILoggerFactory loggerFactory,
            IThermometerServices thermometerServices) : base(loggerFactory)
        {
            this._thermometerServices = thermometerServices;
        }

        public override async Task HandleAsync(ThermometerValuesUpdated @event, CancellationToken cancellationToken = new CancellationToken())
        {
            if (cancellationToken.IsCancellationRequested)
                cancellationToken.ThrowIfCancellationRequested();

            await this._thermometerServices.AppendValuesAsync(@event.DeviceId, @event.DeviceName, @event.Temperature,
                @event.CommunicationDate);
        }
    }
}