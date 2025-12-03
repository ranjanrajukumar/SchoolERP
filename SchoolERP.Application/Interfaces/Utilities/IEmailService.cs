using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolERP.Application.Interfaces.Utilities
{
    public interface IEmailService
    {
        Task SendEmailAsync(string to, string subject, string body);
    }
}
