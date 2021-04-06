using CloudGenThermometer.Messages.Events;
using FourSolid.Athena.Factories;
using FourSolid.Athena.Messages;
using FourSolid.Mercurio.Azure.Factories;
using Microsoft.Extensions.DependencyInjection;

namespace CloudGenThermometer.Mediator
{
    public static class DeviceMessagesHelper
    {
        public static IServiceCollection AddDeviceMessagesProcessor(this IServiceCollection services, string servicebusConnectionString)
        {
            services.AddScoped(provider =>
            {
                var domainEventHandlerFactory = provider.GetService<IDomainEventHandlerFactoryAsync>();

                var brokerOptions = new BrokerOptions
                {
                    ConnectionString = servicebusConnectionString,
                    TopicName = nameof(ThermometerValuesUpdated).ToLower(),
                    SubscriptionName = "CloudGen-Subscription"
                };

                var domainEventConsumerFactory =
                    new ServiceBusEventProcessorFactory<ThermometerValuesUpdated>(brokerOptions, domainEventHandlerFactory);
                return domainEventConsumerFactory.DomainEventProcessorAsync;
            });

            return services;
        }
    }
}