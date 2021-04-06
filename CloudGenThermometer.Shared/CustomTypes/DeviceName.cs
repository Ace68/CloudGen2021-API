using CloudGenThermometer.Shared.Abstracts;

namespace CloudGenThermometer.Shared.CustomTypes
{
    public sealed class DeviceName : CustomTypeString<DeviceName>
    {
        public DeviceName(string value) : base(value)
        {
        }
    }
}