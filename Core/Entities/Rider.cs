using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities.Identity;

namespace Core.Entities
{
    public class Rider : BaseEntity
    {
       [Required]
        public string AppUserId { get; set; }
        public AppUser AppUser{get;set;}
        public string AccountNumber{get;set;}
        public string BankCode{get;set;}
        public string BVN{get;set;}
         public string ValidID { get; set; }

    }
}