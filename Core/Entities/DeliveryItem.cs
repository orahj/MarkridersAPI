using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Enum;

namespace Core.Entities
{
    public class DeliveryItem : BaseEntity
    {
        public DeliveryItem()
        {
        }

        public DeliveryItem(string pickUpItems, decimal deliveryAmount, DeliveryTpe deliveryTpe, DeliveryTime deliveryTime, Carriers carriers, string pickUpPhone, string dropOffPhone, string imageUrl,int deliveryId,int deliveryLocationId)
        {
            PickUpItems = pickUpItems;
            DeliveryAmount = deliveryAmount;
            DeliveryTpe = deliveryTpe;
            DeliveryTime = deliveryTime;
            Carriers = carriers;
            PickUpPhone = pickUpPhone;
            DropOffPhone = dropOffPhone;
            ImageUrl = imageUrl;
            DeliveryLocationId = deliveryLocationId;
            DeliveryId = deliveryId;
        }

        public string PickUpItems { get; set; }
        public decimal DeliveryAmount { get; set; } 
        public DateTimeOffset DeliveryDate { get; set; } = DateTimeOffset.Now;
        public DeliveryTpe DeliveryTpe { get; set; }
        public DeliveryTime DeliveryTime { get; set; }
        public DeliveryStatus DeliveryStatus{get;set;} = DeliveryStatus.Processing;
        public Carriers Carriers { get; set; }
        public string PickUpPhone { get; set; }
        public string DropOffPhone { get; set; }
        public string ImageUrl {get;set;}
        public int DeliveryId{ get; set; }
        public Delivery Delivery {get;set;}
        public int DeliveryLocationId {get;set;}
        public DeliveryLocation DeliveryLocation {get;set;}
         //public virtual IReadOnlyList<DeliveryLocation> DeliveryLocations { get; set; }

    }
}