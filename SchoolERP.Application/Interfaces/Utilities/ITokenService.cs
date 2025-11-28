using SchoolERP.Domain.Entities.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolERP.Application.Interfaces.Utilities
{
    public interface ITokenService
    {
        string GenerateToken(string userId, string email, string role);
    }

}
