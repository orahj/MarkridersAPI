using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarkriderAPI.Controllers.DTOS.Payment
{
    public class VerifyTransaction
    {
         public string TransactionRef { get; set; }
        public decimal amount { get; set; }
        public Guid Email { get; set; }
    }
}