using CloudGenThermometer.ApplicationServices.Handlers;
using CloudGenThermometer.Messages.Events;
using FourSolid.Athena.Messages.Events;
using Microsoft.Extensions.DependencyInjection;

namespace CloudGenThermometer.Mediator
{
    public static class EventHandlerHelper
    {
        public static IServiceCollection AddEventHandler(this IServiceCollection services)
        {
            services
                .AddScoped<IDomainEventHandlerAsync<ThermometerValuesUpdated>, ThermometerValuesUpdatedEventHandler>();

            return services;
        }
    }
}