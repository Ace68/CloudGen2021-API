using CloudGenThermometer.Shared.Abstracts;

namespace CloudGenThermometer.Shared.CustomTypes
{
    public sealed class UnitOfMeasurement : CustomTypeString<UnitOfMeasurement>
    {
        public UnitOfMeasurement(string value) : base(value)
        {
        }
    }
}