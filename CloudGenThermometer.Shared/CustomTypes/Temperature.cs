using CloudGenThermometer.Shared.Abstracts;

namespace CloudGenThermometer.Shared.CustomTypes
{
    public sealed class Temperature : CustomTypeBase<Temperature>
    {
        public readonly double Value;

        public Temperature(double value)
        {
            this.Value = value;
        }
    }
}