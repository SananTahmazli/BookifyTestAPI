using DataAccess.Entities;
using MailKit.Net.Smtp;
using MimeKit;
using Repositories.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Concretes
{
    public class EmailRepository : IEmailRepository
    {
        private readonly EmailConfiguration _emailConfiguration;

        public EmailRepository(EmailConfiguration emailConfiguration)
        {
            _emailConfiguration = emailConfiguration;
        }

        public void SendEmail(Message message)
        {
            var emailMessage = CreateEmail(message);
            SendMessage(emailMessage);
        }

        private MimeMessage CreateEmail(Message message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("company.bookify@gmail.com", _emailConfiguration.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message.Content
            };
            return emailMessage;
        }

        private void SendMessage(MimeMessage mailMessage)
        {
            var client = new SmtpClient();
            try
            {
                client.Connect(_emailConfiguration.SmtpServer, _emailConfiguration.Port, true);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate(_emailConfiguration.Username, _emailConfiguration.Password);
                client.Send(mailMessage);
            }
            catch
            {
                throw new Exception("Error occured!");
            }
            finally
            {
                client.Disconnect(true);
                client.Dispose();
            }
        }
    }
}