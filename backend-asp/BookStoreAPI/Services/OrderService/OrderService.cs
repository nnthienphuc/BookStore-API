using BookStoreAPI.Common.Helpers;
using BookStoreAPI.Data.Entities;
using BookStoreAPI.Services.OrderService.DTOs;
using BookStoreAPI.Services.OrderService.Interfaces;
using BookStoreAPI.Services.OrderService.Repositories;
using System.Runtime.CompilerServices;
using System.Security.Claims;

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
                StaffName = o.Staff.FamilyName + " " + o.Staff.GivenName,
                CustomerName = o.Customer.FamilyName + " " + o.Customer.GivenName,
                PromotionName = o.Promotion?.Name,
                StaffId = o.Staff.Id,
                CustomerId = o.Customer.Id,
                PromotionId = o.Promotion?.Id,
                CreatedTime = o.CreatedTime,
                TotalAmount = o.TotalAmount,
                SubTotalAmount = o.SubTotalAmount,
                PromotionAmount = o.PromotionAmount,
                Status = o.Status,
                Note = o.Note,
                IsDeleted = o.IsDeleted
            });
        }

        public async Task<OrderDTO?> GetByIdAsync(Guid id, ClaimsPrincipal user)
        {
            var order = await _orderRepository.GetByIdAsync(id);

            if (order == null)
                throw new KeyNotFoundException($"Không tìm thấy đơn hàng với ID '{id}'.");

            var staffId = CurrentUserHelper.GetStaffId(user);
            var isAdmin = CurrentUserHelper.IsAdmin(user);

            if (!isAdmin && order.StaffId != staffId)
                throw new UnauthorizedAccessException("Bạn chỉ có thể xem đơn hàng của chính mình.");

            return new OrderDTO
            {
                Id = order.Id,
                StaffName = order.Staff.FamilyName + ' ' + order.Staff.GivenName,
                CustomerName = order.Customer.FamilyName + ' ' + order.Customer.GivenName,
                CustomerPhone = order.Customer.Phone,
                PromotionName = order.Promotion?.Name,
                CreatedTime = order.CreatedTime,
                TotalAmount = order.TotalAmount,
                SubTotalAmount = order.SubTotalAmount,
                PromotionAmount = order.PromotionAmount,
                Status = order.Status,
                Note = order.Note,
                IsDeleted = order.IsDeleted
            };
        }

        public async Task<IEnumerable<OrderItemDTO>> GetItemsByOrderIdAsync(Guid orderId, ClaimsPrincipal user)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);

            var orderItems = await _orderRepository.GetItemsByOrderIdAsync(orderId);

            var staffId = CurrentUserHelper.GetStaffId(user);
            var isAdmin = CurrentUserHelper.IsAdmin(user);

            if (!isAdmin && order.StaffId != staffId)
                throw new UnauthorizedAccessException("Bạn chỉ có thể xem đơn hàng của chính mình.");

            return orderItems.Select(oi => new OrderItemDTO
            {
                OrderId = orderId,
                BookId = oi.Book.Id,
                BookName = oi.Book.Title,
                Price = oi.Price,
                Quantity = oi.Quantity,
                IsDeleted = oi.IsDeleted
            });
        }

        public async Task<IEnumerable<OrderDTO>> SearchByKeywordAsync(string keyword, ClaimsPrincipal user)
        {
            var staffId = CurrentUserHelper.GetStaffId(user);
            var isAdmin = CurrentUserHelper.IsAdmin(user);

            var orders = await _orderRepository.SearchByKeywordAsync(keyword);

            if (!isAdmin)
                orders = orders.Where(o => o.StaffId == staffId).ToList();

            return orders.Select(o => new OrderDTO
            {
                Id = o.Id,
                StaffName = o.Staff.FamilyName + ' ' + o.Staff.GivenName,
                CustomerName = o.Customer.FamilyName + ' ' + o.Customer.GivenName,
                PromotionName = o.Promotion?.Name,
                CreatedTime = o.CreatedTime,
                TotalAmount = o.TotalAmount,
                SubTotalAmount = o.SubTotalAmount,
                PromotionAmount = o.PromotionAmount,
                Status = o.Status,
                Note = o.Note,
                IsDeleted = o.IsDeleted
            });
        }

        public async Task<bool> AddAsync(OrderCreateDTO orderCreateDTO, ClaimsPrincipal user)
        {
            if (orderCreateDTO.Items == null || !orderCreateDTO.Items.Any())
                throw new ArgumentException("Đơn hàng phải có ít nhất một sản phẩm.");

            var customer = await _orderRepository.GetCustomerByIdAsync(orderCreateDTO.CustomerId);
            if (customer == null || customer.IsDeleted)
                throw new ArgumentException("Khách hàng không hợp lệ hoặc đã bị xoá.");

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
                var book = await _orderRepository.GetBookByIdAsync(item.BookId)
                    ?? throw new KeyNotFoundException($"Không tìm thấy sách với ID '{item.BookId}'.");

                if (book.Quantity < item.Quantity)
                    throw new InvalidOperationException($"Số lượng sách '{book.Title}' không đủ để bán.");

                if (book.IsDeleted)
                    throw new InvalidOperationException($"Sách '{book.Title}' đã bị xoá.");

                var orderItem = new OrderItem
                {
                    OrderId = order.Id,
                    BookId = item.BookId,
                    Quantity = item.Quantity,
                    Price = book.Price
                };

                book.Quantity -= item.Quantity;

                subTotal += (orderItem.Price * orderItem.Quantity);
                order.OrderItems.Add(orderItem);
            }

            order.SubTotalAmount = subTotal;

            if (order.PromotionId.HasValue)
            {
                var promotion = await _orderRepository.GetPromotionByIdAsync(order.PromotionId.Value)
                    ?? throw new ArgumentException("Không tìm thấy khuyến mãi.");

                if (promotion.IsDeleted)
                    throw new InvalidOperationException($"Khuyến mãi '{promotion.Name}' đã bị xoá.");

                if (DateTime.Now < promotion.StartDate)
                    throw new InvalidOperationException("Khuyến mãi chưa bắt đầu.");

                if (DateTime.Now > promotion.EndDate)
                    throw new InvalidOperationException("Khuyến mãi đã hết hạn.");

                if (promotion.Quantity <= 0)
                    throw new InvalidOperationException("Khuyến mãi đã hết lượt sử dụng.");

                if (subTotal < promotion.Condition)
                    throw new InvalidOperationException("Đơn hàng không đủ điều kiện áp dụng khuyến mãi.");

                order.PromotionAmount = subTotal * promotion.DiscountPercent;
                promotion.Quantity--;
            }

            order.TotalAmount = subTotal - order.PromotionAmount;

            await _orderRepository.AddAsync(order);

            return await _orderRepository.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(Guid id, OrderUpdateDTO orderUpdateDTO)
        {
            var order = await _orderRepository.GetByIdAsync(id);

            if (order == null)
                throw new KeyNotFoundException($"Không tìm thấy đơn hàng với ID '{id}'.");

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
                throw new KeyNotFoundException($"Không tìm thấy đơn hàng với ID '{id}'.");

            _orderRepository.Delete(order);

            return await _orderRepository.SaveChangesAsync();
        }

        public async Task<bool> DeleteItem(Guid orderId, Guid bookId)
        {
            var item = await _orderRepository.GetOrderItemByOrderIdAndBookIdAsync(orderId, bookId);

            if (item == null)
                throw new KeyNotFoundException("Không tìm thấy sản phẩm trong đơn hàng.");

            _orderRepository.DeleteItem(item);

            return await _orderRepository.SaveChangesAsync();
        }
    }
}
