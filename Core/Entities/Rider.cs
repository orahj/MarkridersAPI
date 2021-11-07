using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Rider : BaseEntity
    {
        public int AppUserId{get;set;}
        public AppUser AppUser{get;set;}
        public string AccountNumber{get;set;}
        public string BankCode{get;set;}
        public string BVN{get;set;}
         public string ValidID { get; set; }

    }
}