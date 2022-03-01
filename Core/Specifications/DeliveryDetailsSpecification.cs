using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class DeliveryDetailsSpecification : BaseSpecification<DeliveryDetails>
    {
        public DeliveryDetailsSpecification(string userId) : base(x => x.AppUserId == userId)
        {
            AddInclude(x => x.AppUser);
            AddInclude(x => x.Ratings);
            AddInclude(x => x.Deliveries);
            AddOrderByDecending(x => x.DateCreated);
        }

        public DeliveryDetailsSpecification(int deliveryId) : base(x => x.DeliveriesId == deliveryId)
        {
        }
    }
}
