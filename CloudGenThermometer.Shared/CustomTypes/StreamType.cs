using CloudGenThermometer.Shared.Abstracts;

namespace CloudGenThermometer.Shared.CustomTypes
{
    public sealed class StreamType : CustomTypeString<StreamType>
    {
        public StreamType(string value) : base(value)
        {
        }
    }
}