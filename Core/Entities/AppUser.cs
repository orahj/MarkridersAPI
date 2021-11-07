using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Enum;

namespace Core.Entities
{
    public class AppUser : BaseEntity
    {
        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Avatar { get; set; }
        public bool IsActive { get; set; }
        public UserTypes UserTypes {get; set; }
        public DateTime DateRegistered { get; set; }
        public Gender Gender { get; set; }
        public string StateId { get; set; }
        public State State { get; set; }
        public int CountryId { get; set; }
        public Country Country { get; set; }
    }
}