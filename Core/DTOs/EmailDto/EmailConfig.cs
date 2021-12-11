using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs.EmailDto
{
    public class EmailConfig
    {
        public string FromName { get; set; }
        public string FromAddress { get; set; }
        public string LocalDomain { get; set; }
        public string MailServerAddress { get; set; }
        public string MailServerPort { get; set; }
        public bool RequiresAuthentication { get; set; }
        public string UserId { get; set; }
        public string UserPassword { get; set; }
        public string VerificationEndPoint { get; set; }
        public string ResetPasswordEndPoint { get; set; }
        public string SendGridKey { get; set; }
    }
}
