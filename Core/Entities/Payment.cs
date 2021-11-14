using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Enum;
using Core.Entities.Identity;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class Payment : BaseEntity
    {
        [Required]
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public string SerialNumber { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime? PaymentDate { get; set; }
        public bool Paid { get; set; }
        public string TransactionRef { get; set; }
        public int TransactionsId { get; set; }
        public Transaction Transaction { get; set; }
    }
}