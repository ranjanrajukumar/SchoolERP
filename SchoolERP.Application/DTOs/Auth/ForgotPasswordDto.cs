using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolERP.Application.DTOs.Auth
{
    public class ForgotPasswordDto
    {
        public class ForgotPasswordRequest
        {
            public string Email { get; set; }
        }

        public class ResetPasswordRequest
        {
            public string Email { get; set; }
            public string Token { get; set; }
            public string NewPassword { get; set; }
        }
    }
}
