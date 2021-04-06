using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CloudGenThermometer.Controllers.v1
{
    /// <summary>
    /// 
    /// </summary>
    [Route("v1/[controller]")]
    public class WakeupController : BaseController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="loggerFactory"></param>
        public WakeupController(ILoggerFactory loggerFactory) : base(loggerFactory)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<string> SayHello() => new ActionResult<string>("Hello from Banco API");
    }
}