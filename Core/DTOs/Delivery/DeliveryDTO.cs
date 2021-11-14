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
}