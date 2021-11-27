using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.DTOs.Wallet
{
    public class WalletTransactionResponseDto
    {
         public Guid TransactionId { get; set; }
        public Guid WalletId { get; set; }
        public Guid ApplicationUserId { get; set; }
        public int TransactionTypeId { get; set; }
        public string TransactionType { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Description { get; set; }
        public string TransactionReference { get; set; }
        public int WalletTransactionStatusId { get; set; }
        public string WalletTransactionStatus { get; set; }
    }
}