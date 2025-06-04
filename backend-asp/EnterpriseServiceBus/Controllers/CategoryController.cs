using System.Net;
using System.Net.Http.Headers;
using EnterpriseServiceBus.DTOs;
using EnterpriseServiceBus.DTOs.Category;
using EnterpriseServiceBus.Entities;
using EnterpriseServiceBus.Helpers;
using Microsoft.AspNetCore.Mvc;
namespace EnterpriseServiceBus.Controllers;

public class CategoryController : BaseController
{
    private readonly HttpClient client;
    public CategoryController(HttpClient _client)
    {
        client = _client;
    }
    [HttpGet]

    public async Task<IActionResult> GetAll()
    {
        var token = HttpContext.Request.Headers["Authorization"].ToString();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Replace("Bearer ", ""));
        var response = await client.GetFromJsonAsync<List<Category>>($"{UrlHelper.BookStoreApiHost}/api/Category");
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CategoryCreateDTO categoryCreateDTO)
    {
        var token = HttpContext.Request.Headers["Authorization"].ToString();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Replace("Bearer ", ""));
        var response = await client.PostAsJsonAsync($"{UrlHelper.BookStoreApiHost}/api/Category", categoryCreateDTO);
        var result = await response.Content.ReadFromJsonAsync<ApiMessageResponse>();
        if (response.StatusCode == HttpStatusCode.OK)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] CategoryUpdateDTO categoryUpdateDTO)
    {
        var token = HttpContext.Request.Headers["Authorization"].ToString();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Replace("Bearer ", ""));
        var response = await client.PutAsJsonAsync($"{UrlHelper.UserServicesHost}/api/Category/{id}", categoryUpdateDTO);
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
        var response = await client.DeleteAsync($"{UrlHelper.UserServicesHost}/api/Category/{id}");
        var result = await response.Content.ReadFromJsonAsync<ApiMessageResponse>();
        if (response.StatusCode == HttpStatusCode.OK)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }
}
