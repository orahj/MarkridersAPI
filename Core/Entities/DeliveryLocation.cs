using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class DeliveryLocation : BaseEntity
    {
        public DeliveryLocation()
        {
        }

        public DeliveryLocation(string baseAddress, double xLogitude, double xLatitude, double deliveryDistance, double yLogitude, double yLatitude, string targetAddress,int deliveryItemId)
        {
            BaseAddress = baseAddress;
            XLogitude = xLogitude;
            XLatitude = xLatitude;
            DeliveryDistance = deliveryDistance;
            YLogitude = yLogitude;
            YLatitude = yLatitude;
            TargetAddress = targetAddress;
            DeliveryItemId = deliveryItemId;
        }

        public string BaseAddress { get; set; }
        public double XLogitude { get; set; }
        public double XLatitude { get; set; }
        public double DeliveryDistance { get; set; }
         public double YLogitude { get; set; }
        public double YLatitude { get; set; }
         public string TargetAddress { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public int DeliveryItemId{get;set;}
        public DeliveryItem DeliveryItem{get;set;}
    }
}