using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Specifications
{
    public class DeliveryDistanceSpec : BaseSpecification<DeliveryDistance>
    {
        public DeliveryDistanceSpec(int  id) : base( x=>x.Id == id)
        { 
        }
    }
}