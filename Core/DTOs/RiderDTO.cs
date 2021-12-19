using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class RiderDTO
    {
        public string AppUserId { get; set; }
        public string AccountNumber{get;set;}
        public string BankCode{get;set;}
        public string BVN{get;set;}
         public string ValidID { get; set; }
    }
    public class RiderStatusDTO
    {
        public string UserId { get; set; }
        public bool Status { get; set; }
    }
}