using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarkriderAPI.Controllers.DTOS
{
    public class DeliveryDto
    {
        public string DeliveryNo { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime DateCreated { get; set; }
        public string Email { get; set; }
       // public virtual ICollection<DeliveryItem> DeliveryItems { get; set; }
    }
}