using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Enum;

namespace Core.Entities
{
    public class Delivery : BaseEntity
    {
        public string DeliveryNo { get; set; }
        public int AppUserId { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime DateCreated { get; set; }
        public AppUser AppUser { get; set; }
        public virtual ICollection<DeliveryItem> DeliveryItems { get; set; }
    }
}