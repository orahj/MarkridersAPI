using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarkriderAPI.Controllers.DTOS
{
    public class DeliveryDto
    {
        public string DeliveryNo { get; set; }
        public int AppUserId { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime DateCreated { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Avatar { get; set; }
       // public virtual ICollection<DeliveryItem> DeliveryItems { get; set; }
    }
}