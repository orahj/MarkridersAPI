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

        public Transaction(decimal amount, decimal? amountWithCharge, int deliveriesId)
        {
            DateCreated = DateTime.UtcNow;
            Amount = amount;
            AmountWithCharge = amountWithCharge;
            DeliveriesId = deliveriesId;
        }

        public DateTime DateCreated { get; set; }
        public decimal Amount { get; set; }
        public decimal? AmountWithCharge { get; set; }
        public int DeliveriesId { get; set; }
        public Delivery Deliveries { get; set; }
    }
}