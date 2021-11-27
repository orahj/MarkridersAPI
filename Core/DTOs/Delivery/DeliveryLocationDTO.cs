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
    public class DeliveryLocationReturnDTO
    {
         public string BaseAddress { get; set; }
        public double XLogitude { get; set; }
        public double XLatitude { get; set; }
        public double DeliveryDistance { get; set; }
         public double YLogitude { get; set; }
        public double YLatitude { get; set; }
         public string TargetAddress { get; set; }
        public string DeliveryItemName{get;set;}
         public IReadOnlyList<DeliveryItemReturnDTO> DeliveryItems{get;set;}
    }
}