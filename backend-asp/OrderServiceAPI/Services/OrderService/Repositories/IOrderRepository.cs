using OrderServiceAPI.Data.Entities;

namespace BookStoreAPI.Services.OrderService.Repositories;

public interface IOrderRepository
{
    Task<IEnumerable<Order>> GetAllAsync();
    Task<Order?> GetByIdAsync(Guid id);
    Task<IEnumerable<OrderItem>> GetItemsByOrderIdAsync(Guid orderId);
    Task AddAsync(Order order);
    void Update(Order order);
    void Delete(Order order);
    void DeleteItem(OrderItem item);
    Task<bool> SaveChangesAsync();
}
