using Core.DTOs.EmailDto;
using Core.Interfaces;
using MarkriderAPI.Email.Interfaces;
using Microsoft.Extensions.Options;
using MimeKit;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MarkriderAPI.Email.Implementations
{
    public class MailClient : IMailClient
    {
        private readonly EmailConfig ec;
        private readonly ISecurityService _security;

        public MailClient(IOptions<EmailConfig> emailConfig, ISecurityService security)
        {
            ec = emailConfig.Value;
            _security = security ?? throw new ArgumentNullException(nameof(security));
        }
        private List<MimePart> CreateImageAttachments(List<MailAttachment> attachments)
        {
            if (attachments == null) return null;

            var final = new List<MimePart>();

            foreach (MailAttachment attach in attachments)
            {
                var attachment = new MimePart(attach.ContentType)
                {
                    Content = new MimeContent(new MemoryStream(attach.Bytes), ContentEncoding.Base64),
                    ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                    ContentTransferEncoding = ContentEncoding.Base64,
                    FileName = string.IsNullOrEmpty(attach.Filename) ? _security.GetGuid() : attach.Filename
                };

                final.Add(attachment);
            }

            return final;
        }
        private async Task PushMailAsync(List<string> recipients, string subject, string message, List<MailAttachment> attachments, List<string> CC, List<string> BCC)
        {
            try
            {
                var attachment = CreateImageAttachments(attachments);
                var apiKey = ec.SendGridKey;
                var client = new SendGridClient(apiKey);
                var from = new EmailAddress(ec.FromAddress, ec.FromName);
                var to = recipients.Select(x => MailHelper.StringToEmailAddress(x)).ToList();
                var sub = subject;
                var htmlContent = message;
                var plantContent = message;
                var msg = MailHelper.CreateSingleEmailToMultipleRecipients(from, to, sub, plantContent, htmlContent);
                var response = await client.SendEmailAsync(msg);


            }
            catch (Exception ex)
            {

            }
        }
        public async Task SendEmailAsync(string recipient, string subject, string message, List<MailAttachment> attachments, List<string> CC, List<string> BCC)
        {
            await SendEmailAsync(new List<string>() { recipient }, subject, message, attachments, CC, BCC);
        }

        public async Task SendEmailAsync(List<string> recipients, string subject, string message, List<MailAttachment> attachments, List<string> CC, List<string> BCC)
        {
            await Task.Factory.StartNew(async () => {
                await PushMailAsync(recipients, subject, message, attachments, CC, BCC);
            });
        }
    }
}
