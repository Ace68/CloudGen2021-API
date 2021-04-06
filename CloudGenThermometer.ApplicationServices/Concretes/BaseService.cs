using CloudGenThermometer.ReadModel.Abstracts;
using Microsoft.Extensions.Logging;

namespace CloudGenThermometer.ApplicationServices.Concretes
{
    public abstract class BaseService
    {
        protected IPersister Persister;
        protected ILogger Logger;

        protected BaseService(IPersister persister, ILoggerFactory loggerFactory)
        {
            this.Persister = persister;
            this.Logger = loggerFactory.CreateLogger(this.GetType());
        }
    }
}