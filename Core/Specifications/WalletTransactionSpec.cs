using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Specifications
{
    public class WalletTransactionSpec : BaseSpecification<WalletTransaction>
    {
        public WalletTransactionSpec(int  walletId) : base( x=>x.WalletId == walletId){}
    }
}