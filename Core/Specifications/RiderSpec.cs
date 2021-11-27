using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Specifications
{
    public class RiderSpec : BaseSpecification<Rider>
    {
        public RiderSpec(string  userId) : base( x=>x.AppUserId == userId){}
    }
}