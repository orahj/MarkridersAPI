using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class RidersDelivery : BaseEntity
    {
        public int DeliveryID {get;set;}
        public Delivery Delivery{get;set;}
        public int RiderId{get;set;}
        public Rider Rider{get;set;}
        public DateTime DateAsigned{get;set;}
        
    }
}