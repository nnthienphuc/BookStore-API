using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderServiceAPI.Services.OrderService.DTOs;
using OrderServiceAPI.Services.OrderService.Interfaces;

namespace OrderServiceAPI.Controllers
{
    public class OrderController : BaseController
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _orderService.GetAllAsync(User);

                return Ok(result);
            }
            catch (Exception ex) {
                return Ok();
            }

        }


        [HttpGet("items/{orderId}")]
        public async Task<IActionResult> GetItemsByOrderId(Guid orderId)
        {
            var result = await _orderService.GetItemsByOrderIdAsync(orderId, User);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] OrderCreateDTO orderCreateDTO)
        {
            var result = await _orderService.AddAsync(orderCreateDTO, User);

            return Ok(new { message = "Order created successfully." });
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] OrderUpdateDTO orderUpdateDTO)
        {
            var result = await _orderService.UpdateAsync(id, orderUpdateDTO);

            return Ok(new { message = "Order updated successfully." });
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _orderService.DeleteAsync(id);

            return Ok(new { message = "Order soft deleted successfully." });
        }

    }
}
