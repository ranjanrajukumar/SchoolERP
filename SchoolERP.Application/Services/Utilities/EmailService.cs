using SchoolERP.Application.Interfaces.Utilities;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace SchoolERP.Application.Services.Utilities
{
    public class EmailService:IEmailService
    {
        public async Task SendEmailAsync(string to, string subject, string body)
        {
            var smtp = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("your-email@gmail.com", "app-password"),
                EnableSsl = true
            };

            var message = new MailMessage("your-email@gmail.com", to, subject, body)
            {
                IsBodyHtml = true
            };

            await smtp.SendMailAsync(message);
        }
    }
}
