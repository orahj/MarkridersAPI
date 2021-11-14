using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Specifications
{
    public class DeliveryWithFilterCountSpec : BaseSpecification<Delivery>
    {
        public DeliveryWithFilterCountSpec(string sort, string email, SpecParams specParams)
            :base(x =>
                (string.IsNullOrEmpty(specParams.Search) || x.DeliveryNo.ToLower().Contains(specParams.Search)) &&
                (!string.IsNullOrEmpty(email) || x.Email == email)
            )
        {
        }
    }
}