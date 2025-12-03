using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolERP.Application.DTOs.Utilities;
using SchoolERP.Application.Interfaces.Utilities;
using static SchoolERP.Application.DTOs.Auth.ForgotPasswordDto;

namespace SchoolERP.API.Controllers.Utilities
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IUserAccountService _userAccountService;
        public AuthController(IUserService userService, IUserAccountService userAccountService)
        {
            _userService = userService;
            _userAccountService = userAccountService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginDto)
        {
            var result = await _userService.AuthenticateAsync(loginDto);
            if (result == null)
                return Unauthorized(new { message = "Invalid credentials" });

            return Ok(result);
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordRequest request)
        {
            var result = await _userAccountService.SendForgotPasswordLinkAsync(request);

            if (!result)
                return NotFound(new { message = "Email not found" });

            return Ok(new { message = "Reset link sent to your email" });
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest request)
        {
            var result = await _userAccountService.ResetPasswordAsync(request);

            if (!result)
                return BadRequest(new { message = "Invalid or expired token" });

            return Ok(new { message = "Password reset successful" });
        }
    }
}
