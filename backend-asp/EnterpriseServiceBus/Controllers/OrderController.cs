using EnterpriseServiceBus.DTOs;
using System.Net.Http.Headers;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EnterpriseServiceBus.Entities;
using EnterpriseServiceBus.Helpers;


namespace EnterpriseServiceBus.Controllers;

public class OrderController : BaseController
{


    private readonly HttpClient client;
    public OrderController(HttpClient _client)
    {
        client = _client;
    }
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var token = HttpContext.Request.Headers["Authorization"].ToString();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Replace("Bearer ", ""));
        var response = await client.GetFromJsonAsync<List<Order>>($"{UrlHelper.OrderApiHost}/api/Order");
        return Ok(response);
    }
    [HttpGet("items/{orderId}")]
    public async Task<IActionResult> GetItemsByOrderId(Guid orderId)
    {
        var token = HttpContext.Request.Headers["Authorization"].ToString();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Replace("Bearer ", ""));
        var response = await client.GetFromJsonAsync<List<OrderItem>>($"{UrlHelper.OrderApiHost}/api/Order/items/{orderId}");
        return Ok(response);
    }
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] OrderCreateDTO orderCreateDTO)
    {
            var token = HttpContext.Request.Headers["Authorization"].ToString();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Replace("Bearer ", ""));
            var bookResponse = await client.PostAsJsonAsync($"{UrlHelper.BookStoreApiHost}/api/Book/UpdateOrder", orderCreateDTO.Items);
            var promotionResponse = await client.GetAsync($"{UrlHelper.BookStoreApiHost}/api/Promotion/UpdateOrder/{orderCreateDTO.PromotionId}");
            var customerResponse = await client.GetAsync($"{UrlHelper.UserServicesHost}/api/Customer/{orderCreateDTO.CustomerId}");
            if (bookResponse.StatusCode == HttpStatusCode.OK && promotionResponse.StatusCode == HttpStatusCode.OK && customerResponse.StatusCode == HttpStatusCode.OK)
            {
                var resultPromotion = await promotionResponse.Content.ReadFromJsonAsync<ApiMessageResponse>();
                orderCreateDTO.DiscountPercent = decimal.Parse(resultPromotion.Message);
                var response = await client.PostAsJsonAsync($"{UrlHelper.OrderApiHost}/api/Order", orderCreateDTO);
                var result = await response.Content.ReadFromJsonAsync<ApiMessageResponse>();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }

            else { return BadRequest(new { message = "Error when update." }); }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] OrderUpdateDTO orderUpdateDTO)
    {
        var token = HttpContext.Request.Headers["Authorization"].ToString();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Replace("Bearer ", ""));
        var response = await client.PutAsJsonAsync($"{UrlHelper.OrderApiHost}/api/Order/{id}", orderUpdateDTO);
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
        var response = await client.DeleteAsync($"{UrlHelper.OrderApiHost}/api/Order/{id}");
        var result = await response.Content.ReadFromJsonAsync<ApiMessageResponse>();
        if (response.StatusCode == HttpStatusCode.OK)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }
}
