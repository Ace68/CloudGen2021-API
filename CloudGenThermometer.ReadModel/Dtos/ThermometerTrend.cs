using System;
using CloudGenThermometer.ReadModel.Abstracts;
using CloudGenThermometer.Shared.CustomTypes;
using CloudGenThermometer.Shared.JsonModel;

namespace CloudGenThermometer.ReadModel.Dtos
{
    public class ThermometerTrend : DtoBase
    {
        public string DeviceId { get; private set; }
        public string DeviceName { get; private set; }
        public DateTime CommunicationDate { get; private set; }
        public double Temperature { get; private set; }
        
        protected ThermometerTrend()
        { }

        #region ctor
        public ThermometerTrend(DeviceId deviceId, DeviceName deviceName, CommunicationDate communicationDate,
            Temperature temperature)
        {
            this.Id = Guid.NewGuid().ToString();

            this.DeviceId = deviceId.ToString();
            this.DeviceName = deviceName.Value;

            this.CommunicationDate = communicationDate.Value;
            this.Temperature = temperature.Value;
        }
        #endregion

        public ThermometerTrendJson ToJson()
        {
            return new ThermometerTrendJson
            {
                DeviceId = this.DeviceId,
                DeviceName = this.DeviceName,
                
                CommunicationDate = this.CommunicationDate,
                Temperature = this.Temperature
            };
        }
    }
}