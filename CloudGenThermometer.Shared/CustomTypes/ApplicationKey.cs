using FourSolid.Common.DomainObjectsBase;

namespace CloudGenThermometer.Shared.CustomTypes
{
    public sealed class ApplicationKey : ValueObjectString<ApplicationKey>
    {
        public ApplicationKey(string value) : base(value)
        {
        }
    }
}