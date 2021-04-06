using System;
using FourSolid.Athena.Core;

namespace CloudGenThermometer.Shared.CustomTypes
{
    public sealed class DeviceId : DomainId
    {
        public DeviceId(Guid value) : base(value)
        {
        }

        public override string ToString() => this.Value.ToString();
    }
}