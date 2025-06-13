using BookStoreAPI.Common.Controllers;
using BookStoreAPI.Services.AuthService.DTOs;
using BookStoreAPI.Services.AuthService.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Controllers
{
    [AllowAnonymous]
    public class AuthController : BaseController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
        {
            var success = await _authService.RegisterAsync(registerDTO);

            if (!success)
                return BadRequest("Đăng ký không thành công. Vui lòng kiểm tra thông tin của bạn.");

            return Ok("Đăng ký thành công. Vui lòng kiểm tra email để kích hoạt tài khoản của bạn.");
        }

        [HttpGet("activate")]
        public async Task<IActionResult> ActivateAccount([FromQuery] string token)
        {
            var success = await _authService.ActivateAccountAsync(token);

            if (!success)
                return BadRequest("Kích hoạt không thành công.");

            return Ok("Tài khoản đã được kích hoạt thành công.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            try
            {
                var token = await _authService.LoginAsync(loginDTO);

                return Ok(new { token });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDTO resetPasswordDTO)
        {
            try
            {
                await _authService.ResetPasswordAsync(resetPasswordDTO);
                return Ok(new { message = "Email đặt lại mật khẩu đã được gửi thành công." });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("reset-password")]
        public async Task<IActionResult> ResetPassword([FromQuery] string token)
        {
            var success = await _authService.ResetPasswordFromTokenAsync(token);

            if (!success)
                return BadRequest( new { message = "Token không hợp lệ hoặc đã hết hạn." });

            return Ok(new { message = "Đã đặt lại mật khẩu thành công. Mật khẩu mới của bạn là: '123456'" });
        }

        [HttpPut("change-password")]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDTO changePasswordDTO)
        {
            try
            {
                var staffIdClaim = User.FindFirst("staffId");

                if (staffIdClaim == null)
                    return Unauthorized(new { message = "Token không hợp lệ." });

                var staffId = Guid.Parse(staffIdClaim.Value);

                var success = await _authService.ChangePasswordAsync(staffId, changePasswordDTO);

                if (!success)
                    return BadRequest(new { message = "Đổi mật khẩu không thành công. Vui lòng kiểm tra thông tin của bạn." });

                return Ok(new { message = "Đã thay đổi mật khẩu thành công." });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
