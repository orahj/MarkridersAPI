using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Specifications
{
    public class WalletSpec : BaseSpecification<Wallet>
    {
         public WalletSpec(string  userId) : base( x=>x.AppUserId == userId){}
    }
}