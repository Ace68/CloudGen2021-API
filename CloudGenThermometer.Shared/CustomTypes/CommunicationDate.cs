using System;
using CloudGenThermometer.Shared.Abstracts;

namespace CloudGenThermometer.Shared.CustomTypes
{
    public sealed class CommunicationDate : CustomTypeDate<CommunicationDate>
    {
        public CommunicationDate(DateTime value) : base(value)
        {
        }
    }
}