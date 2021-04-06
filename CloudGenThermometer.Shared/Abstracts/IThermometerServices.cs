using System.Collections.Generic;
using System.Threading.Tasks;
using CloudGenThermometer.Shared.CustomTypes;
using CloudGenThermometer.Shared.JsonModel;

namespace CloudGenThermometer.Shared.Abstracts
{
    public interface IThermometerServices
    {
        Task AppendValuesAsync(DeviceId deviceId, DeviceName deviceName, Temperature temperature,
            CommunicationDate communicationDate);

        Task<IEnumerable<ThermometerTrendJson>> GetThermometerTrendAsync(DeviceId deviceId);
    }
}