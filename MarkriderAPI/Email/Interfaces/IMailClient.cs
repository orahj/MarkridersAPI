using Core.DTOs.EmailDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MarkriderAPI.Email.Interfaces
{
    public interface IMailClient
    {
        Task SendEmailAsync(string recipient, string subject, string message, List<MailAttachment> attachments, List<string> CC, List<string> BCC);
        Task SendEmailAsync(List<string> recipients, string subject, string message, List<MailAttachment> attachments, List<string> CC, List<string> BCC);
    }
}
