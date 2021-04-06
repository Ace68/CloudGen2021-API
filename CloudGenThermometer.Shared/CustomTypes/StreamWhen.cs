using System;
using CloudGenThermometer.Shared.Abstracts;

namespace CloudGenThermometer.Shared.CustomTypes
{
    public sealed class StreamWhen : CustomTypeDate<StreamWhen>
    {
        public StreamWhen(DateTime value) : base(value)
        {
        }
    }
}