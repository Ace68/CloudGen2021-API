using CloudGenThermometer.Shared.Abstracts;

namespace CloudGenThermometer.Shared.CustomTypes
{
    public sealed class EventId : CustomTypeString<EventId>
    {
        public EventId(string value) : base(value)
        {
        }
    }
}