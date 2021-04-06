using System;

namespace CloudGenThermometer.Shared.JsonModel
{
    public class ThermometerTrendJson
    {
        public string DeviceId { get; set; }
        public string DeviceName { get; set; }
        public DateTime CommunicationDate { get; set; }
        public double Temperature { get; set; }
    }
}