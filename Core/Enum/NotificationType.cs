using System.Runtime.Serialization;

namespace Core.Enum
{
    public enum NotificationType
    {
        [EnumMember(Value = "User Update")]
        UserInfoUpdate,
        [EnumMember(Value = "Payment")]
        PaymentUpdate,
        [EnumMember(Value = "Wallet")]
        WalletUpdate,
        [EnumMember(Value = "Delivery")]
        DeliveryUpdate,
        [EnumMember(Value = "General")]
        General
    }
}
