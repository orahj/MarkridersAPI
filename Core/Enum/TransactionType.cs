using System.Runtime.Serialization;

namespace Core.Enum
{
    public enum TransactionType
    {
        [EnumMember(Value = "Fund Request")]
        FundRequest,
         [EnumMember(Value = "Wallet TopUp")]
        WalletTopUp
    }
}
