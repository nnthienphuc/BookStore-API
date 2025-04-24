using BookStoreAPI.Common.Controllers;
using BookStoreAPI.Services.AuthService.DTOs;
using BookStoreAPI.Services.AuthService.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Services.AuthService.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
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
                return BadRequest("Registration failed. Please check your information.");

            return Ok("Registration successful. Please check your email to activate your account.");
        }

        [HttpGet("activate")]
        public async Task<IActionResult> ActivateAccount([FromQuery] string token)
        {
            var success = await _authService.ActivateAccountAsync(token);

            if (!success)
                return BadRequest("Activation failed. Please check your token.");

            return Ok("Account activated successfully.");
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
        }
    }
}
