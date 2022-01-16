using Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class DeliveryDetails : BaseEntity
    {
        public DeliveryDetails()
        {
        }

        public DeliveryDetails(string appUserId, int deliveriesId, string status, decimal amount)
        {
            AppUserId = appUserId;
            DeliveriesId = deliveriesId;
            Deliverystatus = status;
            DeliveryAmount = amount;
        }

        [Required]
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public int DeliveriesId { get; set; }
        public decimal DeliveryAmount { get; set; }
        public Delivery Deliveries { get; set; }
        public bool IsCanceled { get; set; }
        public bool IsDisputed { get; set; }
        public string Deliverystatus { get; set; }
        public string DisputedComment { get; set; }
        public string DisputedUser { get; set; }
        public string CancelReason { get; set; }
        public string Canceleduser { get; set; }
        public bool IsCompleted { get; set; }
        public int? RatingId { get; set; }
        public Ratings Ratings { get; set; } 
    }
}
