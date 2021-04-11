using FourSolid.Common.DomainObjectsBase;

namespace CloudGenThermometer.Shared.CustomTypes
{
    public sealed class EventName : ValueObjectString<MessageContent>
    {
        public EventName(string value) : base(value)
        {
        }
    }
}