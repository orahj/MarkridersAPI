using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Specifications
{
    public class RIderGuarantorSpec : BaseSpecification<RiderGuarantor>
    {
         public RIderGuarantorSpec(int  riderId) : base( x=>x.RiderId == riderId){}
    }
}