using FourSolid.Common.DomainObjectsBase;

namespace CloudGenThermometer.Shared.CustomTypes
{
    public sealed class MessageContent : ValueObjectString<MessageContent>
    {
        public MessageContent(string value) : base(value)
        {
        }
    }
}