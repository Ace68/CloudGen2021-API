using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CloudGenThermometer.Controllers.v1
{
    /// <inheritdoc />
    /// <summary>
    /// 
    /// </summary>
    public class HomeController : BaseController
    {
        public HomeController(ILoggerFactory loggerFactory)
            : base(loggerFactory)
        {
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return this.View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult Get()
        {
            return this.Ok();
        }
    }
}