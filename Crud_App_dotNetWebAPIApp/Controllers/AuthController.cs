using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Crud_App_dotNetApplication.DTOs.Auth;
using Crud_App_dotNetApplication.Interfaces.IServices;

namespace Crud_App_dotNetWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterDTO dto)
        {
            if (dto == null) return BadRequest(new { message = "Registration data is required." });
            var result = await _authService.RegisterAsync(dto);
            if (result == null) return BadRequest(new { message = "User already exists or registration failed." });
            return Ok(result);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginDTO dto)
        {
            if (dto == null) return BadRequest(new { message = "Login data is required." });
            var result = await _authService.LoginAsync(dto);
            if (result == null) return Unauthorized(new { message = "Invalid credentials." });
            return Ok(result);
        }

        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            await _authService.LogoutAsync(token);
            return Ok(new { message = "Logged out." });
        }

        [HttpPost("send-email-verification")]
        [AllowAnonymous]
        public async Task<IActionResult> SendEmailVerification([FromBody] string usernameOrEmail)
        {
            if (string.IsNullOrWhiteSpace(usernameOrEmail)) return BadRequest(new { message = "Username or email is required." });
            var ok = await _authService.SendEmailVerificationAsync(usernameOrEmail);
            if (!ok) return NotFound(new { message = "User not found." });
            return Ok(new { message = "Verification email sent." });
        }

        [HttpPost("verify-email")]
        [AllowAnonymous]
        public async Task<IActionResult> VerifyEmail([FromBody] string token)
        {
            if (string.IsNullOrWhiteSpace(token)) return BadRequest(new { message = "Token is required." });
            var ok = await _authService.VerifyEmailAsync(token);
            if (!ok) return BadRequest(new { message = "Invalid or expired token." });
            return Ok(new { message = "Email verified." });
        }

        [HttpPost("send-password-reset")]
        [AllowAnonymous]
        public async Task<IActionResult> SendPasswordReset([FromBody] string usernameOrEmail)
        {
            if (string.IsNullOrWhiteSpace(usernameOrEmail)) return BadRequest(new { message = "Username or email is required." });
            var ok = await _authService.SendPasswordResetAsync(usernameOrEmail);
            if (!ok) return NotFound(new { message = "User not found." });
            return Ok(new { message = "Password reset email sent." });
        }

        [HttpPost("reset-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPasswordDTO dto)
        {
            if (dto == null) return BadRequest(new { message = "Data is required." });
            var ok = await _authService.ResetPasswordAsync(dto);
            if (!ok) return BadRequest(new { message = "Invalid or expired token." });
            return Ok(new { message = "Password reset successful." });
        }
    }
}
