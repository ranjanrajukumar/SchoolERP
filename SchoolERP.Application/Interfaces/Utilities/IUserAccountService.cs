using System;
using System.Collections.Generic;
using System.Text;
using static SchoolERP.Application.DTOs.Auth.ForgotPasswordDto;

namespace SchoolERP.Application.Interfaces.Utilities
{
    public interface IUserAccountService
    {
        Task<bool> SendForgotPasswordLinkAsync(ForgotPasswordRequest request);
        Task<bool> ResetPasswordAsync(ResetPasswordRequest request);
    }
}
