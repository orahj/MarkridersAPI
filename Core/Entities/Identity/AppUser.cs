using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Enum;
using Microsoft.AspNetCore.Identity;

namespace Core.Entities.Identity
{
    public class AppUser : IdentityUser
    {
        public byte[] PasswordSalt { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Avatar { get; set; }
        public bool IsActive { get; set; }
        public UserTypes UserTypes {get; set; }
        public DateTime DateRegistered { get; set; }
        public Gender Gender { get; set; }
        public int StateId { get; set; }
        public State State { get; set; }
        public int CountryId { get; set; }
        public Country Country { get; set; }
        public int Percentage{ get; set; }
        public UserCategory UserCategory { get; set; }
        public string CompanyName{get;set;}
        public string RCNumber{get;set;}
        public string BusinessName{get;set;}
        public string BusinessNumber{get;set;}

    }
}