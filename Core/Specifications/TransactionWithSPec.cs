using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Specifications
{
    public class TransactionWithSPec : BaseSpecification<Transaction>
    {
         public TransactionWithSPec(int  id) : base( x=>x.Id == id){}
    }
}