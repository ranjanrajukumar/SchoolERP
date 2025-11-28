using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolERP.Application.DTOs.Utilities
{
    public class LoginResponseDto
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
