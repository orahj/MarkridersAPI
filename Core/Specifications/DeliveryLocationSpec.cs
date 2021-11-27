using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Specifications
{
    public class DeliveryLocationeSpec : BaseSpecification<DeliveryLocation>
    {
        public DeliveryLocationeSpec()
        {
            AddInclude(x => x.DeliveryItems);            
        }

        public DeliveryLocationeSpec(int  id) : base( x=>x.Id == id)
        {
            AddInclude(x => x.DeliveryItems); 
        }
    }
}