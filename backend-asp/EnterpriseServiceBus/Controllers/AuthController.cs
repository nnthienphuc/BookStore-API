
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using EnterpriseServiceBus.DTOs;
using EnterpriseServiceBus.DTOs.Auth;
using EnterpriseServiceBus.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseServiceBus.Controllers;

[AllowAnonymous]
public class AuthController : BaseController
{
    private readonly HttpClient client;
    public AuthController(HttpClient _client)
    {
        client = _client;
    }
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
    {
        var response = await client.PostAsJsonAsync($"{UrlHelper.UserServicesHost}/api/Auth/register", registerDTO);
        var result = await response.Content.ReadFromJsonAsync<ApiMessageResponse>();
        if (response.StatusCode == HttpStatusCode.OK)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }

    [HttpGet("activate")]
    public async Task<IActionResult> ActivateAccount([FromQuery] string token)
    {
        var response = await client.GetAsync($"{UrlHelper.UserServicesHost}/api/Auth/activate?token={token}");
        var result = await response.Content.ReadFromJsonAsync<ApiMessageResponse>();
        if (response.StatusCode == HttpStatusCode.OK)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
    {
        var response = await client.PostAsJsonAsync($"{UrlHelper.UserServicesHost}/api/Auth/login", loginDTO);

        if (response.StatusCode == HttpStatusCode.OK)
        {
            var json = await response.Content.ReadAsStringAsync();
            var doc = JsonDocument.Parse(json);
            var token = doc.RootElement.GetProperty("token").GetString();
            return Ok(new { token });
        }
        else {
            var result = await response.Content.ReadFromJsonAsync<ApiMessageResponse>();
            return BadRequest(result);
        }
        
    }
    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDTO resetPasswordDTO)
    {
        var response = await client.PostAsJsonAsync($"{UrlHelper.UserServicesHost}/api/Auth/reset-password", resetPasswordDTO);
        var result = await response.Content.ReadFromJsonAsync<ApiMessageResponse>();
        if (response.StatusCode == HttpStatusCode.OK)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }

    [HttpGet("reset-password")]
    public async Task<IActionResult> ResetPassword([FromQuery] string token)
    {
        var response = await client.GetAsync($"{UrlHelper.UserServicesHost}/api/Auth/reset-password?token={token}");
        var result = await response.Content.ReadFromJsonAsync<ApiMessageResponse>();
        if (response.StatusCode == HttpStatusCode.OK)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }

    [HttpPut("change-password")]
    [Authorize]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDTO changePasswordDTO)
    {
        var token = HttpContext.Request.Headers["Authorization"].ToString();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Replace("Bearer ", ""));
        var response = await client.PutAsJsonAsync($"{UrlHelper.UserServicesHost}/api/Auth/change-password", changePasswordDTO);
        var result = await response.Content.ReadFromJsonAsync<ApiMessageResponse>();
        if (response.StatusCode == HttpStatusCode.OK)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }
}
