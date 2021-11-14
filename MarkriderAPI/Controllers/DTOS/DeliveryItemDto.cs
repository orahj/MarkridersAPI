using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarkriderAPI.Controllers.DTOS
{
    public class DeliveryItemDto
    {
         public string PickUpItems { get; set; }
        public decimal DeliveryAmount { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string DeliveryTpe { get; set; }
        public String DeliveryTime { get; set; }
        public String DeliveryStatus{get;set;}
        public String Carriers { get; set; }
        public string PickUpPhone { get; set; }
        public string DropOffPhone { get; set; }
        public DateTime DateCreated {get; set;}
        public string FileURL{get;set;}
         public string BaseAddress { get; set; }
        public double XLogitude { get; set; }
        public double XLatitude { get; set; }
        public double DeliveryDistance { get; set; }
         public double YLogitude { get; set; }
        public double YLatitude { get; set; }
        public string TargetAddress { get; set; }

    }
}