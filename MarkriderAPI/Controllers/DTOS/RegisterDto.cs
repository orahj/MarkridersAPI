using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Enum;

namespace MarkriderAPI.Controllers.DTOS
{
    public class RegisterDto
    {

        public string UserName{get;set;}
        [Required]
        [EmailAddress]
        public string Email{get;set;}
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Address { get; set; }
        public string Avatar { get; set; }
        public bool IsActive { get; set; }
        [Required]
        [RegularExpression("(?=^.{6,10}$)(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&amp;*()_+}{&quot;:;'?/&gt;.&lt;,])(?!.*\\s).*$", 
        ErrorMessage ="Password must contain 1 uppsercase, 1 lowercase , 1 alphanumeric and atleast 6 characters")]
        public string Password{get;set;}
        public UserTypes UserTypes {get; set; }
        public Gender Gender { get; set; }
        [Required]
        public int State { get; set; }
        [Required]
        public int Country { get; set; }
        public string Phone{get;set;}
        public UserCategory UserCategory { get; set; }
        public string RiderCardNo { get; set; }
    }
}