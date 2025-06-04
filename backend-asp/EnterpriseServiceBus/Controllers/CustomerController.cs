using EnterpriseServiceBus.DTOs;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using EnterpriseServiceBus.Helpers;
using EnterpriseServiceBus.DTOs.Customer;
using System.Text.Json;
using System.Net.Http.Headers;
using EnterpriseServiceBus.Entities;

namespace EnterpriseServiceBus.Controllers;

public class CustomerController : BaseController
{

    private readonly HttpClient client;
    public CustomerController(HttpClient _client)
    {
        client = _client;
    }
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var token = HttpContext.Request.Headers["Authorization"].ToString();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Replace("Bearer ", ""));
        var response = await client.GetFromJsonAsync<List<Customer>>($"{UrlHelper.UserServicesHost}/api/Customer");
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CustomerCreateDTO customerCreateDTO)
    {
        var token = HttpContext.Request.Headers["Authorization"].ToString();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Replace("Bearer ", ""));
        var response = await client.PostAsJsonAsync($"{UrlHelper.UserServicesHost}/api/Customer", customerCreateDTO);
        var result = await response.Content.ReadFromJsonAsync<ApiMessageResponse>();
        if (response.StatusCode == HttpStatusCode.OK)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] CustomerUpdateDTO customerUpdateDTO)
    {
        var token = HttpContext.Request.Headers["Authorization"].ToString();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Replace("Bearer ", ""));
        var response = await client.PutAsJsonAsync($"{UrlHelper.UserServicesHost}/api/Customer/{id}", customerUpdateDTO);
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
        var response = await client.DeleteAsync($"{UrlHelper.UserServicesHost}/api/Customer/{id}");
        var result = await response.Content.ReadFromJsonAsync<ApiMessageResponse>();
        if (response.StatusCode == HttpStatusCode.OK)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }
}
