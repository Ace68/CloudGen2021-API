using CloudGenThermometer.Shared.Abstracts;

namespace CloudGenThermometer.Shared.CustomTypes
{
    public sealed class StreamData : CustomTypeBase<StreamData>
    {
        public readonly byte[] Value;
        
        public StreamData(byte[] value)
        {
            this.Value = value;
        }
    }
}