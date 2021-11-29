using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Enum;

namespace Core.DTOs.Delivery
{
    public class DeliveryItemDTO
    {
        public string PickUpItems { get; set; }
        public DeliveryTpe DeliveryTpe { get; set; }
        public DeliveryTime DeliveryTime { get; set; }
        public Carriers Carriers { get; set; }
        public string PickUpPhone { get; set; }
        public string DropOffPhone { get; set; }
        public int DeliveryId { get; set; }
        public string ImageUrl {get;set;}
        public DeliveryLocationDTO BaseLocation { get; set; }
        public DeliveryLocationDTO TargetLocation { get; set; }
    }
     public class DeliveryItemReturnDTO
    {
        public int Id {get; set;}
        public string PickUpItems { get; set; }
        public string DeliveryTpe { get; set; }
        public string DeliveryTime { get; set; }
        public string Carriers { get; set; }
        public string PickUpPhone { get; set; }
        public string DropOffPhone { get; set; }
        public int DeliveryId { get; set; }
        public string ImageUrl {get;set;}
        public string DeliveryStatus{get;set;}
        public DeliveryLocationReturnDTO DeliveryLocation{get;set;}
    }
}