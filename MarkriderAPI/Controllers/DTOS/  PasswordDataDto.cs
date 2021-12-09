using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarkriderAPI.Controllers.DTOS
{
    public class PasswordDataDto
    {
        public Guid UserId { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string Email { get; set; }
        public string BrowserName { get; set; }
        public string OperatingSystem { get; set; }
        public string Token { get; set; }
    }
    public class SendPasswordResetDto 
    {
        public string Email { get; set; }
    }
    public class ResetPasswordResetDto
    {
        public string Token { get; set; }
        public string NewPassword { get; set; }
        public string Email { get; set; }
    }
}