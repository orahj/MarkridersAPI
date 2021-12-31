using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Core.DTOs.Delivery
{
    public class DeliveryDTO
    {
        [Required]
        public string Email { get; set; }
        public virtual ICollection<DeliveryItemDTO> DeliveryItems { get; set; }
    }
    public class DeliveryReturnDTO
    {
        public int Id {get;set;}
        public string DeliveryNo { get; set; }
        public string Email { get; set; }
        public decimal TotalAmount { get; set; }
        public int? transactionId { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public virtual IReadOnlyList<DeliveryItemReturnDTO> DeliveryItems { get; set; }
    }
    public class DeliveryDetailDTO
    {
        public string AppUserId { get; set; }
        public int DeliveriesId { get; set; }
        public string Reason { get; set; }
    }
}