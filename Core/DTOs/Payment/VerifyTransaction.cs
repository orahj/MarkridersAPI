using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.DTOs.Payment
{
    public class VerifyTransaction
    {
         public string TransactionRef { get; set; }
         public int TransactionId { get; set;}
        public decimal amount { get; set; }
        public string Email { get; set; }
        public string UserId {get; set;}
    }
}