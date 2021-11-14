using System.Runtime.Serialization;

namespace Core.Enum
{
    public enum PaymentMethod
    {
        [EnumMember(Value = "Paystack")]
        Paystack = 1,
        [EnumMember(Value = "Wallet")]
        Wallet = 2,
        [EnumMember(Value = "Transfer")]
        Transfer = 3
    }
}
