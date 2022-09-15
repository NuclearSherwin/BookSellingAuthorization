using System;
using System.IO;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;

namespace bookselling.Utils
{
    public class SendMailService : ISendMailService
    {
        private readonly MailSetting _mailSetting;
        private readonly ILogger<SendMailService> _logger;

        public SendMailService(IOptions<MailSetting> mailSetting, ILogger<SendMailService> logger)
        {
            _mailSetting = mailSetting.Value;
            _logger = logger;
            _logger.LogInformation("Create SendMailService");
        }

        public async Task SendMail(MailContent mailContent)
        {
            var email = new MimeMessage();
            email.Sender = new MailboxAddress(_mailSetting.DisplayName, _mailSetting.Mail);
            email.From.Add(new MailboxAddress(_mailSetting.DisplayName, _mailSetting.Mail));
            email.To.Add(MailboxAddress.Parse(mailContent.To));
            email.Subject = mailContent.Subject;


            var builder = new BodyBuilder();
            builder.HtmlBody = mailContent.Body;
            email.Body = builder.ToMessageBody();

            using var smtp = new MailKit.Net.Smtp.SmtpClient(); 

            try
            {
                smtp.Connect(_mailSetting.Host, _mailSetting.Post, SecureSocketOptions.StartTls);
                smtp.Authenticate(_mailSetting.Mail, _mailSetting.Password);
                
                // send mail
                await smtp.SendAsync(email);
            }
            catch (Exception ex)
            {
                Directory.CreateDirectory("mailsserver");
                var emailsavefile = string.Format(@"mailssave/{0}.eml", Guid.NewGuid());
                await email.WriteToAsync(emailsavefile);
                
                _logger.LogInformation("Error send mail at: " + emailsavefile);
                _logger.LogError(ex.Message);
            }
            
            smtp.Disconnect(true);
            
            _logger.LogInformation("Send mail to " + mailContent.To);
        }

        public async Task SendMailAsync(string email, string subject, string htmlMessage)
        {
            await SendMail(new MailContent()
            {
                To = email,
                Subject = subject,
                Body = htmlMessage
            });
        }
        
    }
}