using System.Runtime.Serialization;

namespace Core.Enum
{
    public enum DeliveryTime
    {
        [EnumMember(Value = "Right Away")]
        RigthAway = 1,
        [EnumMember(Value = "Schedule For Later")]
        ScheduledLater = 2
    }
}
