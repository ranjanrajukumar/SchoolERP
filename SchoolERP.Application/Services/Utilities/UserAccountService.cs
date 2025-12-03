using SchoolERP.Application.Interfaces.Utilities;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using static SchoolERP.Application.DTOs.Auth.ForgotPasswordDto;

namespace SchoolERP.Application.Services.Utilities
{
    public class UserAccountService : IUserAccountService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _email;

        public UserAccountService(IUserRepository userRepository, IEmailService email)
        {
            _userRepository = userRepository;
            _email = email;
        }

        public async Task<bool> SendForgotPasswordLinkAsync(ForgotPasswordRequest request)
        {
            var user = await _userRepository.GetUserByEmailAsync(request.Email);
            if (user == null) return false;

            string token = WebUtility.UrlEncode(Guid.NewGuid().ToString());

            user.ResetToken = token;
            user.ResetTokenExpire = DateTime.UtcNow.AddHours(1);

            await _userRepository.UpdateAsync(user);

            string resetUrl = $"https://your-frontend/reset-password?email={user.Email}&token={token}";

            string body = $@"
                <h3>Password Reset</h3>
                <p>Click link to reset password:</p>
                <a href='{resetUrl}'>Reset Password</a>
            ";

            await _email.SendEmailAsync(user.Email, "Reset Password", body);

            return true;
        }

        public async Task<bool> ResetPasswordAsync(ResetPasswordRequest request)
        {
            var user = await _userRepository.GetUserByTokenAsync(request.Email, request.Token);
            if (user == null || user.ResetTokenExpire < DateTime.UtcNow)
                return false;

            user.Password = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
            user.ResetToken = null;
            user.ResetTokenExpire = null;

            await _userRepository.UpdateAsync(user);

            return true;
        }
    }
}