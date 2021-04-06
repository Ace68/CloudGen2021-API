using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CloudGenThermometer.Controllers
{
    /// <inheritdoc />
    /// <summary>
    /// </summary>
    public abstract class BaseController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        protected ILogger Logger { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="loggerFactory"></param>
        protected BaseController(ILoggerFactory loggerFactory)
        {
            this.Logger = loggerFactory.CreateLogger(this.GetType());
        }
    }
}
