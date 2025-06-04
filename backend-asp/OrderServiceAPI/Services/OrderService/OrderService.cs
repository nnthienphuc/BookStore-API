using System.Security.Claims;
using BookStoreAPI.Services.OrderService.Repositories;
using OrderServiceAPI.Common.Helpers;
using OrderServiceAPI.Data.Entities;
using OrderServiceAPI.Services.OrderService.DTOs;
using OrderServiceAPI.Services.OrderService.Interfaces;

namespace BookStoreAPI.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<IEnumerable<OrderDTO>> GetAllAsync(ClaimsPrincipal user)
        {
            var staffId = CurrentUserHelper.GetStaffId(user);
            var isAdmin = CurrentUserHelper.IsAdmin(user);

            var orders = await _orderRepository.GetAllAsync();

            if (!isAdmin)
                orders = orders.Where(o => o.StaffId == staffId).ToList();

            return orders.Select(o => new OrderDTO
            {
                Id = o.Id,            
                StaffId = o.StaffId,
                CustomerId = o.CustomerId,
                PromotionId = o.PromotionId,
                CreatedTime = o.CreatedTime,
                TotalAmount = o.TotalAmount,
                SubTotalAmount = o.SubTotalAmount,
                PromotionAmount = o.PromotionAmount,
                Status = o.Status,
                Note = o.Note,
                IsDeleted = o.IsDeleted
            });
        }

        public async Task<IEnumerable<OrderItemDTO>> GetItemsByOrderIdAsync(Guid orderId, ClaimsPrincipal user)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);

            var orderItems = await _orderRepository.GetItemsByOrderIdAsync(orderId);

            var staffId = CurrentUserHelper.GetStaffId(user);
            var isAdmin = CurrentUserHelper.IsAdmin(user);

            if (!isAdmin && order.StaffId != staffId)
                throw new UnauthorizedAccessException("You can only view your own orders.");

            return orderItems.Select(oi => new OrderItemDTO
            {
                OrderId = orderId,
                BookId = oi.BookId,
                Price = oi.Price,
                Quantity = oi.Quantity,
                IsDeleted = oi.IsDeleted
            });
        }

        public async Task<bool> AddAsync(OrderCreateDTO orderCreateDTO, ClaimsPrincipal user)
        {
            if (orderCreateDTO.Items == null || !orderCreateDTO.Items.Any())
                throw new ArgumentException("Order must have at least one item.");         
            var order = new Order
            {
                Id = Guid.NewGuid(),
                StaffId = CurrentUserHelper.GetStaffId(user),
                CustomerId = orderCreateDTO.CustomerId,
                PromotionId = orderCreateDTO.PromotionId,
                CreatedTime = DateTime.Now,
                Status = true
            };

            decimal subTotal = 0;

            foreach (var item in orderCreateDTO.Items)
            {
                
                var orderItem = new OrderItem
                {
                    OrderId = order.Id,
                    BookId = item.BookId,
                    Quantity = item.Quantity,
                    Price = item.Price
                };

                

                subTotal += (orderItem.Price * orderItem.Quantity);
                order.OrderItems.Add(orderItem);
            }

            order.SubTotalAmount = subTotal;
            order.PromotionAmount = subTotal * orderCreateDTO.DiscountPercent;           

            order.TotalAmount = subTotal - order.PromotionAmount;

            await _orderRepository.AddAsync(order);

            return await _orderRepository.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(Guid id, OrderUpdateDTO orderUpdateDTO)
        {
            var order = await _orderRepository.GetByIdAsync(id);

            if (order == null)
                throw new KeyNotFoundException($"Order with id '{id}' not found");

            order.Status = orderUpdateDTO.Status;
            order.Note = orderUpdateDTO.Note;
            order.IsDeleted = orderUpdateDTO.IsDeleted;

            _orderRepository.Update(order);

            return await _orderRepository.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var order = await _orderRepository.GetByIdAsync(id);

            if (order == null)
                throw new KeyNotFoundException($"Order with id '{id}' not found");

            _orderRepository.Delete(order);

            return await _orderRepository.SaveChangesAsync();
        }
    }
}
