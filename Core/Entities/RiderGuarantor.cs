using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class RiderGuarantor : BaseEntity
    {
        public int RiderId {get;set;}
        public Rider Rider {get;set;}
        public string FirstName{get;set;}
        public string LastName{get;set;}
         public string NIN{get;set;}
    }
}