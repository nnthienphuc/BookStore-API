
using EnterpriseServiceBus.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Net;
using EnterpriseServiceBus.Entities;
using EnterpriseServiceBus.Helpers;
using EnterpriseServiceBus.DTOs.Book;
using EnterpriseServiceBus.DTOs.Author;

namespace EnterpriseServiceBus.Controllers;

public class AuthorController : BaseController
{

    private readonly HttpClient client;
    public AuthorController(HttpClient _client)
    {
        client = _client;
    }
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var token = HttpContext.Request.Headers["Authorization"].ToString();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Replace("Bearer ", ""));
        var response = await client.GetFromJsonAsync<List<Author>>($"{UrlHelper.BookStoreApiHost}/api/Author");
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] AuthorCreateDTO authorCreateDTO)
    {
        var token = HttpContext.Request.Headers["Authorization"].ToString();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Replace("Bearer ", ""));
        var response = await client.PostAsJsonAsync($"{UrlHelper.BookStoreApiHost}/api/Author", authorCreateDTO);
        var result = await response.Content.ReadFromJsonAsync<ApiMessageResponse>();
        if (response.StatusCode == HttpStatusCode.OK)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] AuthorUpdateDTO authorUpdateDTO)
    {
        var token = HttpContext.Request.Headers["Authorization"].ToString();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Replace("Bearer ", ""));
        var response = await client.PutAsJsonAsync($"{UrlHelper.UserServicesHost}/api/Author/{id}", authorUpdateDTO);
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
        var response = await client.DeleteAsync($"{UrlHelper.UserServicesHost}/api/Author/{id}");
        var result = await response.Content.ReadFromJsonAsync<ApiMessageResponse>();
        if (response.StatusCode == HttpStatusCode.OK)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }
}
