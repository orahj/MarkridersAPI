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

        public DeliveryItem(string pickUpItems, decimal deliveryAmount, DeliveryTpe deliveryTpe, DeliveryTime deliveryTime, Carriers carriers, string pickUpPhone, string dropOffPhone, int fileDataId,int deliveryId)
        {
            PickUpItems = pickUpItems;
            DeliveryAmount = deliveryAmount;
            DeliveryTpe = deliveryTpe;
            DeliveryTime = deliveryTime;
            Carriers = carriers;
            PickUpPhone = pickUpPhone;
            DropOffPhone = dropOffPhone;
            FileDataId = fileDataId;
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
        public DateTimeOffset DateCreated {get; set;} = DateTimeOffset.Now;
        public int FileDataId {get;set;}
        public FileData FileData {get;set;}
        public int DeliveryId {get;set;}
        public Delivery Delivery {get;set;}
         public virtual IReadOnlyList<DeliveryLocation> DeliveryLocations { get; set; }

    }
}