using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Enum;

namespace Core.Entities
{
    public class WalletTransaction : BaseEntity
    {
        public WalletTransaction()
        {
        }

        public WalletTransaction(int walletId,TransactionType transactionType, decimal amount, string description, string transactionReference, WalletTransactionStatus walletTransactionStatus)
        {
            WalletId = walletId;
            TransactionType = transactionType;
            Amount = amount;
            TransactionDate = DateTime.UtcNow;
            Description = description;
            TransactionReference = transactionReference;
            WalletTransactionStatus = walletTransactionStatus;
        }

        public int WalletId { get; set; }
        public Wallet Wallet { get; set; }
        public TransactionType TransactionType { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Description { get; set; }
        public string TransactionReference { get; set; }
        public WalletTransactionStatus WalletTransactionStatus { get; set; }
    }
}