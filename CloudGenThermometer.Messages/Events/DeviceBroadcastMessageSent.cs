using CloudGenThermometer.Shared.CustomTypes;
using FourSolid.Athena.Messages.Events;
using FourSolid.Common.ValueObjects;

namespace CloudGenThermometer.Messages.Events
{
    public sealed class DeviceBroadcastMessageSent : DomainEvent
    {
        public readonly DeviceId DeviceId;
        public readonly MessageContent MessageContent;
        public readonly EventName EventName;
        public readonly ApplicationKey ApplicationKey;

        public DeviceBroadcastMessageSent(DeviceId aggregateId, MessageContent messageContent, EventName eventName,
            ApplicationKey applicationKey, AccountInfo who, When when) : base(aggregateId, who, when)
        {
            this.DeviceId = aggregateId;
            this.MessageContent = messageContent;
            this.EventName = eventName;
            this.ApplicationKey = applicationKey;
        }
    }
}