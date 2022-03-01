using System.Runtime.Serialization;

namespace Core.Enum
{
    public enum DeliveryStatus
    {
        [EnumMember(Value = "Processing")]
         Processing = 1,
         [EnumMember(Value = "Assigned")]
         Assigned = 2,
         [EnumMember(Value = "Started")]
         Started = 3,
         [EnumMember(Value = "Completed")]
          Completed = 4,
          [EnumMember(Value = "Delivered")]
          Delivered = 5,
          [EnumMember(Value = "Disputed")]
          Disputed = 6,
        [EnumMember(Value = "Canceled")]
        Canceled = 7,
        [EnumMember(Value = "Canceled")]
        Created = 8
    }
}
