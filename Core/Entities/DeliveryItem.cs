using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Enum;

namespace Core.Entities
{
    public class DeliveryItem : BaseEntity
    {
        public string PickUpItems { get; set; }
        public decimal DeliveryAmount { get; set; }
        public DateTime DeliveryDate { get; set; }
        public DeliveryTpe DeliveryTpe { get; set; }
        public DeliveryTime DeliveryTime { get; set; }
        public DeliveryStatus DeliveryStatus{get;set;}
        public Carriers Carriers { get; set; }
        public string PickUpPhone { get; set; }
        public string DropOffPhone { get; set; }
        public DateTime DateCreated {get; set;}
        public int DeliveryId { get; set; }
        public Delivery Delivery { get; set; }
        public int FileDataId {get;set;}
        public FileData FileData {get;set;}
        public int DeliveryLocationId { get; set; }
        public DeliveryLocation DeliveryLocation { get; set; }

    }
}