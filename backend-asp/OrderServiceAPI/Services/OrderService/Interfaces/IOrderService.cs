using OrderServiceAPI.Services.OrderService.DTOs;
using System.Security.Claims;

namespace OrderServiceAPI.Services.OrderService.Interfaces;

public interface IOrderService
{
    Task<IEnumerable<OrderDTO>> GetAllAsync(ClaimsPrincipal user);
    Task<IEnumerable<OrderItemDTO>> GetItemsByOrderIdAsync(Guid orderId, ClaimsPrincipal user);
    Task<bool> AddAsync(OrderCreateDTO orderCreateDTO, ClaimsPrincipal user);
    Task<bool> UpdateAsync(Guid id, OrderUpdateDTO orderUpdateDTO);
    Task<bool> DeleteAsync(Guid id);

}
