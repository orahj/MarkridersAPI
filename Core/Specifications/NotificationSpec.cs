using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class NotificationSpec: BaseSpecification<Notification>
    {
        public NotificationSpec(string userId) : base(x => x.AppUserId == userId && !x.Read)
        {
            AddOrderByDecending(x => x.Id);
            AddOrderBy(x => x.DateCreated);
        }
    }
}
