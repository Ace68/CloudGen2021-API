using System;
using CloudGenThermometer.Messages.Events;
using FourSolid.Athena.Messages.Events;
using Microsoft.Extensions.DependencyInjection;

namespace CloudGenThermometer.Mediator.Subscribers
{
    public static class StartSubscribers
    {
        private static IServiceProvider _serviceProvider;

        private static void InitServiceProvider(IServiceCollection services)
        {
            _serviceProvider ??= services.BuildServiceProvider();
        }

        /// <summary>
        /// Starts events subscribers
        /// </summary>
        /// <param name="services"></param>
        public static void Start(IServiceCollection services)
        {
            InitServiceProvider(services);

            var thermometerValuesUpdatedProcessor =
                _serviceProvider.GetService<IDomainEventProcessorAsync<ThermometerValuesUpdated>>();
            thermometerValuesUpdatedProcessor?.RegisterBroker();
        }
    }
}