using System.Runtime.Serialization;

namespace Core.Enum
{
    public enum Carriers
    {
        [EnumMember(Value = "Bikes")]
        Bikes = 1,
        [EnumMember(Value = "Bus")]
        Bus = 2,
        [EnumMember(Value = "Truck")]
        Truck = 3
    }
}
