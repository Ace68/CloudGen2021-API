using System;
using System.Collections.Generic;
using FourSolid.Athena.Factories;
using FourSolid.Athena.Messages.Events;
using Microsoft.Extensions.DependencyInjection;

namespace CloudGenThermometer.Mediator.Factories
{
    public class IntegrationEventHandlerFactory : IIntegrationEventHandlerFactoryAsync
    {
        private readonly IServiceProvider _serviceProvider;

        public IntegrationEventHandlerFactory(IServiceProvider serviceProvider)
        {
            this._serviceProvider = serviceProvider;
        }

        public IIntegrationEventHandlerAsync<T> CreateIntegrationEventHandlerAsync<T>() where T : class, IIntegrationEvent
        {
            return this._serviceProvider.GetService<IIntegrationEventHandlerAsync<T>>();
        }

        public IEnumerable<IIntegrationEventHandlerAsync<T>> CreateIntegrationEventHandlersAsync<T>() where T : class, IIntegrationEvent
        {
            return this._serviceProvider.GetServices<IIntegrationEventHandlerAsync<T>>();
        }
    }
}