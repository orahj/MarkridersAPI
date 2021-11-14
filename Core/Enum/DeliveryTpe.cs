using System.Runtime.Serialization;

namespace Core.Enum
{
    public enum DeliveryTpe
    {
        [EnumMember(Value = "Single")]
         Single = 1,
         [EnumMember(Value = "Bulk Delivery")]
        BulkDelivery = 2,
        [EnumMember(Value = "Cargo")]
        Cargo = 3,
        [EnumMember(Value = "Inter State")]
        InterState = 4
    }
}
