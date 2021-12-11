using Core.DTOs;
using Core.DTOs.EmailDto;
using Core.Entities.Identity;
using MarkriderAPI.Email.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace MarkriderAPI.Email.Implementations
{
    public class EmailService : IEmailService
    {
        private readonly IMailClient _client;
        private readonly IWebHostEnvironment _env;
        private readonly EmailRecipient _emailRecipient;

        public EmailService(
              IMailClient client,
              IWebHostEnvironment env,
              IOptions<EmailRecipient> emailRecipient)
        {
            _client = client;
            _env = env;
            _emailRecipient = emailRecipient.Value;
        }
        private async Task<string> GetTemplate(string TemplateFileName)
        {
            var webRoot = _env.ContentRootPath; //get wwwroot Folder
            var pathToFile = _env.ContentRootPath
                           + Path.DirectorySeparatorChar.ToString() + "wwwroot"
                           + Path.DirectorySeparatorChar.ToString() + "templates"
                           + Path.DirectorySeparatorChar.ToString() + TemplateFileName;

            var builder = new BodyBuilder();
            using (StreamReader SourceReader = File.OpenText(pathToFile))
            {
                builder.HtmlBody = await SourceReader.ReadToEndAsync();
            }

            return builder.HtmlBody;
        }
        public async Task<Result> SendPasswordResetMailAsync(AppUser user, string link, string helpUrl, string operatingSystem, string browserName)
        {
            string template = await GetTemplate("PasswordReset.html");
            string mailBody = template.Replace("{{Name}}", user.Email)
                                      .Replace("{{URL}}", link);


            string subject = $"Password Reset, {user.Email} - Mark Rider";
            List<string> BCC = _emailRecipient.Managements;
            await _client.SendEmailAsync(user.Email, subject, mailBody, null, null, BCC);

            return new Result { IsSuccessful = true };
        }

        public async Task<Result> SendWelcomeMailAsync(AppUser user, string link)
        {
            string template = await GetTemplate("Welcome.html");
            string mailBody = template.Replace("{{Name}}", user.Email)
                                      .Replace("{{URL}}", link);


            string subject = $"Welcome, {user.Email} - Mark Rider";
            List<string> BCC = _emailRecipient.Managements;
            await _client.SendEmailAsync(user.Email, subject, mailBody, null, null, BCC);

            return new Result { IsSuccessful = true };
        }
    }
}
