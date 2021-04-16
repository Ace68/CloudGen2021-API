using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CloudGenThermometer.Shared.Abstracts;
using CloudGenThermometer.Shared.CustomTypes;
using CloudGenThermometer.Shared.JsonModel;
using CloudGenThermometer.Shared.Services;
using FourSolid.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CloudGenThermometer.Controllers.v1
{
    public class ThermometersController : BaseController
    {
        private readonly IThermometerServices _thermometerServices;

        public ThermometersController(ILoggerFactory loggerFactory,
            IThermometerServices thermometerServices) : base(loggerFactory)
        {
            this._thermometerServices = thermometerServices;
        }

        [HttpGet]
        [Route("devices/{deviceId}")]
        public async Task<ActionResult<IEnumerable<ThermometerTrendJson>>> GetThermometerTrend([FromRoute] string deviceId)
        {
            try
            {
                var trend = await this._thermometerServices.GetThermometerTrendAsync(new DeviceId(deviceId.ToGuid()));
                return new ActionResult<IEnumerable<ThermometerTrendJson>>(trend);
            }
            catch (Exception ex)
            {
                this.Logger.LogError(CommonServices.GetDefaultErrorTrace(ex));
                return this.BadRequest(CommonServices.GetErrorMessage(ex));
            }
        }

        [HttpGet]
        [Route("devices/deviceName/{deviceName}")]
        public async Task<ActionResult<IEnumerable<ThermometerTrendJson>>> GetThermometerTrendByDeviceName([FromRoute] string deviceName)
        {
            try
            {
                var trend = await this._thermometerServices.GetThermometerTrendByDevicenNameAsync(new DeviceName(deviceName));
                return new ActionResult<IEnumerable<ThermometerTrendJson>>(trend);
            }
            catch (Exception ex)
            {
                this.Logger.LogError(CommonServices.GetDefaultErrorTrace(ex));
                return this.BadRequest(CommonServices.GetErrorMessage(ex));
            }
        }
    }
}