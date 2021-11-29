using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Specifications
{
    public class DeliveryItemSpecification : BaseSpecification<DeliveryItem>
    {
        public DeliveryItemSpecification()
        {
            AddInclude(x=>x.DeliveryLocation);           
        }

        public DeliveryItemSpecification(int  id) : base( x=>x.Id == id)
        {  
             AddInclude(x=>x.DeliveryLocation);
        }
         public DeliveryItemSpecification(int  id, int deliveryId) : base( x=>x.Id == id && x.DeliveryId == deliveryId)
        {   
             AddInclude(x=>x.DeliveryLocation);
        }
    }
}