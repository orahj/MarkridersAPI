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
        public Payment()
        {
        }

        public Payment(string appUserId,PaymentMethod paymentMethod, string serialNumber, string invoiceNumber, bool paid, string transactionRef, int transactionsId)
        {
            AppUserId = appUserId;
            PaymentMethod = paymentMethod;
            SerialNumber = serialNumber;
            InvoiceNumber = invoiceNumber;
            Paid = paid;
            TransactionRef = transactionRef;
            TransactionsId = transactionsId;
            PaymentDate = DateTime.UtcNow;
        }

        [Required]
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public string SerialNumber { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime? PaymentDate { get; set; }
        public bool Paid { get; set; }
        public string TransaferpaymentUpload { get; set; }
        public string TransactionRef { get; set; }
        public int TransactionsId { get; set; }
        public Transaction Transaction { get; set; }
    }
}