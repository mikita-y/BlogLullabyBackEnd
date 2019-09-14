using System;
using System.Threading.Tasks;
using BlogLullaby.BLL.Infrastructure;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace BlogLullaby.BLL.EmailService
{
    public class EmailService : IEmailService
    {
        private EmailConfig _emailConfig;
        public EmailService(IOptions<EmailConfig> emailConfig)
        {
            _emailConfig = emailConfig.Value;
        }
        public OperationDetails EmailConfirm(string userEmail)
        {
            throw new NotImplementedException();
        }

        public async Task SendEmailAsync(string email, string subject, string message, string personName = "")
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress(_emailConfig.OrganizationName, _emailConfig.OrganizationEmailAdres));
            emailMessage.To.Add(new MailboxAddress(personName, email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                ///client.ConnectAsync();
                try
                {
                    await client.ConnectAsync(_emailConfig.SmtpServer, _emailConfig.Port, _emailConfig.UseSSL);
                    await client.AuthenticateAsync(_emailConfig.OrganizationEmailAdres, _emailConfig.Password);
                    await client.SendAsync(emailMessage);
                    await client.DisconnectAsync(true);
                }
                catch(Exception e)
                {
                    var a = e.Message;
                }
            }
        }
    }
}
