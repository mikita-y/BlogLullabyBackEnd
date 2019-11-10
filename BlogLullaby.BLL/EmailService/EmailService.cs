using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using BlogLullaby.BLL.Infrastructure;
//using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
//using MimeKit;

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

        public async Task<bool> SendEmailAsync(string email, string subject, string message, string personName = "")
        {
            // отправитель - устанавливаем адрес и отображаемое в письме имя
            MailAddress from = new MailAddress(_emailConfig.OrganizationEmailAdres, _emailConfig.OrganizationName);
            // кому отправляем
            MailAddress to = new MailAddress(email);
            // создаем объект сообщения
            MailMessage m = new MailMessage(from, to);
            // тема письма
            m.Subject = subject;
            // текст письма
            m.Body = message;
            // письмо представляет код html
            m.IsBodyHtml = true;
            // адрес smtp-сервера и порт, с которого будем отправлять письмо
            var a = true;
            SmtpClient smtp = new SmtpClient(_emailConfig.SmtpServer, _emailConfig.Port);
            // логин и пароль
            smtp.Credentials = new NetworkCredential(_emailConfig.OrganizationEmailAdres, _emailConfig.Password);

            smtp.EnableSsl = _emailConfig.UseSSL;
            
                await smtp.SendMailAsync(m);
                return true;
            

            /*
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
                try
                {
                    await client.ConnectAsync(_emailConfig.SmtpServer, _emailConfig.Port, _emailConfig.UseSSL);
                    await client.AuthenticateAsync(_emailConfig.OrganizationEmailAdres, _emailConfig.Password);
                    await client.SendAsync(emailMessage);
                    await client.DisconnectAsync(true);
                    return true;
                }
                catch(Exception e)
                {
                    return false;
                }
            }*/
        }
    }
}
