using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Enum;
using Core.Entities.Identity;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class Delivery : BaseEntity
    {
        public Delivery()
        {
        }

        public Delivery(string deliveryNo, string email, decimal totalAmount = 0)
        {
            DeliveryNo = deliveryNo;
            Email = email;
            TotalAmount = totalAmount;
        }
        public Delivery(int tranId)
        {
            transactionId = tranId;
        }
        public string DeliveryNo { get; set; }
        [Required]
        public string Email { get; set; }
        public decimal TotalAmount { get; set; }
        public bool IscancledByUser { get; set; }
        public string ReasonForCanling { get; set; }
        public int? transactionId { get; set; }
        public Transaction Transaction { get; set; }
        public virtual IReadOnlyList<DeliveryItem> DeliveryItems { get; set; }

        public decimal  GetTotal()
        {
            return DeliveryItems.Sum(x =>x.DeliveryAmount);
        }
    }
}