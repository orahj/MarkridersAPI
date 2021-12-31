using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Transaction : BaseEntity
    {
        public Transaction()
        {
        }

        public Transaction(decimal amount, decimal? amountWithCharge)
        {
            DateCreated = DateTime.UtcNow;
            Amount = amount;
            AmountWithCharge = amountWithCharge;
        }
        public decimal Amount { get; set; }
        public decimal? AmountWithCharge { get; set; }
    }
}