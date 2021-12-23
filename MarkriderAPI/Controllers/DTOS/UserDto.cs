using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Enum;

namespace MarkriderAPI.Controllers.DTOS
{
    public class UserDto
    {
        public string Email{get;set;}
        public string UserName{get;set;}
        public string Token{get;set;}
        public string Id{get;set;}
        public string State{get;set;}
        public string Country{get;set;}
        public string FirstName{get;set;}
        public string LastName{get;set;}
        public UserTypes UserTypes{get;set;}
        public string Avatar {get;set;}
        public string PhoneNumber { get; set; }

    }

    public class PasswordResetResponseDto
    {
        public string Url{get;set;}
    }
    public class VerifyMailDto
    {
         public string Email { get; set; }
        public string Token { get; set; }
    }

    public class EmailVeriyResponseDto
    {
        public string Url{get;set;}
    }
}