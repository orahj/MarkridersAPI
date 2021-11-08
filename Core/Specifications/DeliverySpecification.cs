using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Specifications
{
    public class DeliverySpecification : BaseSpecification<Delivery>
    {
        public DeliverySpecification()
        {
            AddInclude(x => x.AppUser);
            AddInclude(x => x.DeliveryItems);             
        }

        public DeliverySpecification(int  id) : base( x=>x.Id == id)
        {
            AddInclude(x => x.AppUser);
            AddInclude(x => x.DeliveryItems);     
        }
    }
}