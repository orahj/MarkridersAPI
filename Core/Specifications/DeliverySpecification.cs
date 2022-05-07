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
        public DeliverySpecification(string sort, string email, SpecParams specParams)
            :base(x =>
                (string.IsNullOrEmpty(specParams.Search) || x.DeliveryNo.ToLower().Contains(specParams.Search)) &&
                (!string.IsNullOrEmpty(email) || x.Email == email)
            )
        {
            AddInclude(x => x.DeliveryItems);
            AddInclude(x => x.Transaction);
            AddOrderByDecending(x => x.Id);
            if(!string.IsNullOrEmpty(sort))
            {
                switch(sort)
                {
                    case "amountASC":
                        AddOrderBy(a => a.TotalAmount);
                        break;
                    case "amountDesc":
                        AddOrderBy(a => a.TotalAmount);
                        break;
                    default:
                        AddOrderByDecending(d => d.Id);
                        break;
                }
            }
            ApplyPagging(specParams.Pagesize * (specParams.PageIndex - 1), specParams.Pagesize);             
        }

        public DeliverySpecification() : base()
        {
            AddOrderByDecending(x => x.Id);
            AddOrderBy(x => x.DateCreated);
            AddInclude(x => x.DeliveryItems);
            AddInclude(x => x.Transaction);
        }
        public DeliverySpecification(int  id) : base( x=>x.Id == id)
        {
            AddInclude(x => x.DeliveryItems);
            AddInclude(x => x.Transaction);
            AddOrderByDecending(x => x.Id);
        }
        public DeliverySpecification(string email) : base(x => x.Email == email)
        {
            AddInclude(x => x.DeliveryItems);
            AddInclude(x => x.Transaction);
            AddOrderByDecending(x => x.Id);
        }
        public DeliverySpecification(string email, string shipmentNo) : base(x =>x.Email == email && x.DeliveryNo == shipmentNo)
        {
             AddInclude(x => x.DeliveryItems);
            AddInclude(x => x.Transaction);
            AddOrderByDecending(x => x.Id);
        }
    }
}