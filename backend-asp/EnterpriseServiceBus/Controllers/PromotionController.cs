using EnterpriseServiceBus.DTOs;
using System.Net.Http.Headers;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using EnterpriseServiceBus.Entities;
using EnterpriseServiceBus.Helpers;
using EnterpriseServiceBus.DTOs.Promotion;

namespace EnterpriseServiceBus.Controllers;

public class PromotionController : BaseController
{

    private readonly HttpClient client;
    public PromotionController(HttpClient _client)
    {
        client = _client;
    }

    [HttpGet]

    public async Task<IActionResult> GetAll()
    {
        var token = HttpContext.Request.Headers["Authorization"].ToString();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Replace("Bearer ", ""));
        var response = await client.GetFromJsonAsync<List<Promotion>>($"{UrlHelper.BookStoreApiHost}/api/Promotion");
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] PromotionCreateDTO promotionCreateDTO)
    {
        var token = HttpContext.Request.Headers["Authorization"].ToString();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Replace("Bearer ", ""));
        var response = await client.PostAsJsonAsync($"{UrlHelper.BookStoreApiHost}/api/Promotion", promotionCreateDTO);
        var result = await response.Content.ReadFromJsonAsync<ApiMessageResponse>();
        if (response.StatusCode == HttpStatusCode.OK)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] PromotionUpdateDTO promotionUpdateDTO)
    {
        var token = HttpContext.Request.Headers["Authorization"].ToString();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Replace("Bearer ", ""));
        var response = await client.PutAsJsonAsync($"{UrlHelper.UserServicesHost}/api/Promotion/{id}", promotionUpdateDTO);
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
        var response = await client.DeleteAsync($"{UrlHelper.UserServicesHost}/api/Promotion/{id}");
        var result = await response.Content.ReadFromJsonAsync<ApiMessageResponse>();
        if (response.StatusCode == HttpStatusCode.OK)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }
}
