using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Enum;

namespace Core.Specifications 
{
    public class AdditionalChargeWithSpec : BaseSpecification<AdditionalCharges>
    {
         public AdditionalChargeWithSpec(int  paymentMethod) : base( x=>x.PaymentMethod == PaymentMethod.Paystack){}
    }
}