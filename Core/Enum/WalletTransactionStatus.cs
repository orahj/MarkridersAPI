using System.Runtime.Serialization;

namespace Core.Enum
{
    public enum WalletTransactionStatus
    {
        [EnumMember(Value = "Pending")]
        Pending,
         [EnumMember(Value = "Failed")]
        Failed,
         [EnumMember(Value = "Successful")]
        Successful
    }
}
