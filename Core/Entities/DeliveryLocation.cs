using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class DeliveryLocation : BaseEntity
    {
        public string Address { get; set; }
        public double Logitude { get; set; }
        public double Latitude { get; set; }
        public double DeliveryDistance { get; set; }
        public DateTime DateCreated { get; set; }
    }
}