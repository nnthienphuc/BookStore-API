using EnterpriseServiceBus.DTOs;
using System.Net.Http.Headers;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using EnterpriseServiceBus.Helpers;
using EnterpriseServiceBus.DTOs.Staff;
using EnterpriseServiceBus.Entities;

namespace EnterpriseServiceBus.Controllers;

public class StaffController : BaseController
{
    private readonly HttpClient client;
    public StaffController(HttpClient _client)
    {
        client = _client;
    }
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var token = HttpContext.Request.Headers["Authorization"].ToString();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Replace("Bearer ", ""));
        var response = await client.GetFromJsonAsync<List<Staff>>($"{UrlHelper.UserServicesHost}/api/Staff");
        return Ok(response);

    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] StaffUpdateDTO staffUpdateDTO)
    {
        var token = HttpContext.Request.Headers["Authorization"].ToString();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Replace("Bearer ", ""));
        var response = await client.PutAsJsonAsync($"{UrlHelper.UserServicesHost}/api/Staff/{id}", staffUpdateDTO);
        var result = await response.Content.ReadFromJsonAsync<ApiMessageResponse>();
        if (response.StatusCode == HttpStatusCode.OK)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var token = HttpContext.Request.Headers["Authorization"].ToString();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Replace("Bearer ", ""));
        var response = await client.DeleteAsync($"{UrlHelper.UserServicesHost}/api/Staff/{id}");
        var result = await response.Content.ReadFromJsonAsync<ApiMessageResponse>();
        if (response.StatusCode == HttpStatusCode.OK)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }
}
