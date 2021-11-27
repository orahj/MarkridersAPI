using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class RiderGuarantor : BaseEntity
    {
        public RiderGuarantor()
        {
        }
        public RiderGuarantor(int riderId)
        {
            RiderId = riderId;
        }
        public RiderGuarantor(int riderId, string firstName, string lastName, string nIN)
        {
            RiderId = riderId;
            FirstName = firstName;
            LastName = lastName;
            NIN = nIN;
        }

        public int RiderId {get;set;}
        public Rider Rider {get;set;}
        public string FirstName{get;set;}
        public string LastName{get;set;}
         public string NIN{get;set;}
    }
}