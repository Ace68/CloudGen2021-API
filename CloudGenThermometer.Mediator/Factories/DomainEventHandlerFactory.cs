using System;
using System.Collections.Generic;
using FourSolid.Athena.Factories;
using FourSolid.Athena.Messages.Events;
using Microsoft.Extensions.DependencyInjection;

namespace CloudGenThermometer.Mediator.Factories
{
    public class DomainEventHandlerFactory : IDomainEventHandlerFactoryAsync
    {
        private readonly IServiceProvider _serviceProvider;

        public DomainEventHandlerFactory(IServiceProvider serviceProvider)
        {
            this._serviceProvider = serviceProvider;
        }

        public IDomainEventHandlerAsync<T> CreateDomainEventHandlerAsync<T>() where T : class, IDomainEvent
        {
            return this._serviceProvider.GetService<IDomainEventHandlerAsync<T>>();
        }

        public IEnumerable<IDomainEventHandlerAsync<T>> CreateDomainEventHandlersAsync<T>() where T : class, IDomainEvent
        {
            return this._serviceProvider.GetServices<IDomainEventHandlerAsync<T>>();
        }
    }
}