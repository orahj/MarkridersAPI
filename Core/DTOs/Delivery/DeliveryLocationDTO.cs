using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.DTOs.Delivery
{
    public class DeliveryLocationDTO
    {
         public string Address { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }
}