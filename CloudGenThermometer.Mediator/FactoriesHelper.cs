using CloudGenThermometer.Mediator.Factories;
using FourSolid.Athena.Factories;
using Microsoft.Extensions.DependencyInjection;

namespace CloudGenThermometer.Mediator
{
    public static class FactoriesHelper
    {
        public static IServiceCollection AddFactories(this IServiceCollection services)
        {
            services.AddScoped<IDomainEventHandlerFactoryAsync, DomainEventHandlerFactory>();
            services.AddScoped<IIntegrationEventHandlerFactoryAsync, IntegrationEventHandlerFactory>();

            return services;
        }
    }
}