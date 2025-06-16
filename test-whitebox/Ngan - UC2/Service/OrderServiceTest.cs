using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BookStoreAPI.Data.Entities;
using BookStoreAPI.Services.OrderService;
using BookStoreAPI.Services.OrderService.DTOs;
using BookStoreAPI.Services.OrderService.Repositories;
using Moq;

namespace Test.Service
{
    public class OrderServiceTest
    {
        [Fact]
        public async Task AddAsync_WithValidOrderCreateDTO_ReturnsTrue()
        {
            var orderCreateDTO = new OrderCreateDTO()
            {
                PromotionId = null,
                CustomerId = Guid.NewGuid(),
                Items = new List<OrderItemCreateDTO>()
            {
                new OrderItemCreateDTO() { BookId = Guid.NewGuid(), Quantity = 1 },
                new OrderItemCreateDTO() { BookId = Guid.NewGuid(), Quantity = 2 }
            }
            };

            var mockRepository = new Mock<IOrderRepository>();
            var user = new ClaimsPrincipal(new ClaimsIdentity(new[] {
            new Claim("StaffId", Guid.NewGuid().ToString()),
        }, "mock"));

            var customer = new Customer
            {
                Id = orderCreateDTO.CustomerId,
                IsDeleted = false,
                FamilyName = "Thuy",
                GivenName = "Ngan",
                DateOfBirth = DateOnly.FromDateTime(DateTime.Now),
                Address = "97 man thien",
                Phone = "0982286126",
                Gender = false,
            };

            var book1 = new Book
            {
                Id = orderCreateDTO.Items[0].BookId,
                Title = "Test Book 1",
                Price = 150000,
                Quantity = 10,
                IsDeleted = false
            };

            var book2 = new Book
            {
                Id = orderCreateDTO.Items[1].BookId,
                Title = "Test Book 2",
                Price = 150000,
                Quantity = 10,
                IsDeleted = false
            };

            mockRepository.Setup(x => x.GetCustomerByIdAsync(customer.Id)).ReturnsAsync(customer);
            mockRepository.Setup(x => x.GetBookByIdAsync(book1.Id)).ReturnsAsync(book1);
            mockRepository.Setup(x => x.GetBookByIdAsync(book2.Id)).ReturnsAsync(book2);
            mockRepository.Setup(x => x.AddAsync(It.IsAny<Order>())).Returns(Task.CompletedTask);
            mockRepository.Setup(x => x.SaveChangesAsync()).ReturnsAsync(true);

            var orderService = new OrderService(mockRepository.Object);

            var result = await orderService.AddAsync(orderCreateDTO, user);

            Assert.True(result);
            mockRepository.Verify(r => r.AddAsync(It.IsAny<Order>()), Times.Once);
            mockRepository.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task AddAsync_WithNullItems_ThrowsArgumentException()
        {
            var orderService = new OrderService(Mock.Of<IOrderRepository>());

            var dto = new OrderCreateDTO
            {
                Items = null,
                CustomerId = Guid.NewGuid()
            };

            var user = new ClaimsPrincipal();

            await Assert.ThrowsAsync<ArgumentException>(() => orderService.AddAsync(dto, user));
        }

        [Fact]
        public async Task AddAsync_WithEmptyItems_ThrowsArgumentException()
        {
            var orderService = new OrderService(Mock.Of<IOrderRepository>());

            var dto = new OrderCreateDTO
            {
                Items = new List<OrderItemCreateDTO>(),
                CustomerId = Guid.NewGuid()
            };

            var user = new ClaimsPrincipal();

            await Assert.ThrowsAsync<ArgumentException>(() => orderService.AddAsync(dto, user));
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task AddAsync_InvalidCustomer_ThrowsArgumentException(bool isDeleted)
        {
            var mockRepo = new Mock<IOrderRepository>();
            var customerId = Guid.NewGuid();

            mockRepo.Setup(r => r.GetCustomerByIdAsync(customerId))
                    .ReturnsAsync(isDeleted ? new Customer { Id = customerId, IsDeleted = true } : null);

            var dto = new OrderCreateDTO
            {
                CustomerId = customerId,
                Items = new List<OrderItemCreateDTO> { new() { BookId = Guid.NewGuid(), Quantity = 1 } }
            };

            var service = new OrderService(mockRepo.Object);
            var user = new ClaimsPrincipal();

            await Assert.ThrowsAsync<ArgumentException>(() => service.AddAsync(dto, user));
        }

        [Fact]
        public async Task AddAsync_BookNotFound_ThrowsKeyNotFoundException()
        {
            var mockRepo = new Mock<IOrderRepository>();
            var customerId = Guid.NewGuid();
            var bookId = Guid.NewGuid();

            mockRepo.Setup(r => r.GetCustomerByIdAsync(customerId))
                    .ReturnsAsync(new Customer { Id = customerId, IsDeleted = false });

            mockRepo.Setup(r => r.GetBookByIdAsync(bookId))
                    .ReturnsAsync((Book)null);

            var dto = new OrderCreateDTO
            {
                CustomerId = customerId,
                Items = new List<OrderItemCreateDTO> { new() { BookId = bookId, Quantity = 1 } }
            };

            var service = new OrderService(mockRepo.Object);
            var user = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim("StaffId", Guid.NewGuid().ToString()) }));

            await Assert.ThrowsAsync<KeyNotFoundException>(() => service.AddAsync(dto, user));
        }

        [Fact]
        public async Task AddAsync_BookQuantityNotEnough_ThrowsInvalidOperationException()
        {
            var mockRepo = new Mock<IOrderRepository>();
            var customerId = Guid.NewGuid();
            var bookId = Guid.NewGuid();

            mockRepo.Setup(r => r.GetCustomerByIdAsync(customerId))
                    .ReturnsAsync(new Customer { Id = customerId, IsDeleted = false });

            mockRepo.Setup(r => r.GetBookByIdAsync(bookId))
                    .ReturnsAsync(new Book { Id = bookId, Quantity = 0, IsDeleted = false, Price = 100 });

            var dto = new OrderCreateDTO
            {
                CustomerId = customerId,
                Items = new List<OrderItemCreateDTO> { new() { BookId = bookId, Quantity = 1 } }
            };

            var service = new OrderService(mockRepo.Object);
            var user = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim("StaffId", Guid.NewGuid().ToString()) }));

            await Assert.ThrowsAsync<InvalidOperationException>(() => service.AddAsync(dto, user));
        }

        [Fact]
        public async Task AddAsync_BookIsDeleted_ThrowsInvalidOperationException()
        {
            var mockRepo = new Mock<IOrderRepository>();
            var customerId = Guid.NewGuid();
            var bookId = Guid.NewGuid();

            mockRepo.Setup(r => r.GetCustomerByIdAsync(customerId))
                    .ReturnsAsync(new Customer { Id = customerId, IsDeleted = false });

            mockRepo.Setup(r => r.GetBookByIdAsync(bookId))
                    .ReturnsAsync(new Book { Id = bookId, Quantity = 10, IsDeleted = true, Price = 100 });

            var dto = new OrderCreateDTO
            {
                CustomerId = customerId,
                Items = new List<OrderItemCreateDTO> { new() { BookId = bookId, Quantity = 1 } }
            };

            var service = new OrderService(mockRepo.Object);
            var user = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim("StaffId", Guid.NewGuid().ToString()) }));

            await Assert.ThrowsAsync<InvalidOperationException>(() => service.AddAsync(dto, user));
        }

        [Fact]
        public async Task AddAsync_WithValidPromotion_AppliesDiscount()
        {
            var customerId = Guid.NewGuid();
            var bookId = Guid.NewGuid();
            var promotionId = Guid.NewGuid();

            var customer = new Customer { Id = customerId, IsDeleted = false };
            var book = new Book { Id = bookId, Price = 100, Quantity = 10, IsDeleted = false };

            var promotion = new Promotion
            {
                Id = promotionId,
                Quantity = 10,
                DiscountPercent = 0.1m,
                Condition = 50,
                StartDate = DateTime.Now.AddDays(-1),
                EndDate = DateTime.Now.AddDays(1),
                IsDeleted = false
            };

            var mockRepo = new Mock<IOrderRepository>();
            mockRepo.Setup(r => r.GetCustomerByIdAsync(customerId)).ReturnsAsync(customer);
            mockRepo.Setup(r => r.GetBookByIdAsync(bookId)).ReturnsAsync(book);
            mockRepo.Setup(r => r.GetPromotionByIdAsync(promotionId)).ReturnsAsync(promotion);
            mockRepo.Setup(r => r.AddAsync(It.IsAny<Order>())).Returns(Task.CompletedTask);
            mockRepo.Setup(r => r.SaveChangesAsync()).ReturnsAsync(true);

            var dto = new OrderCreateDTO
            {
                CustomerId = customerId,
                PromotionId = promotionId,
                Items = new List<OrderItemCreateDTO> { new() { BookId = bookId, Quantity = 1 } }
            };

            var user = new ClaimsPrincipal(new ClaimsIdentity(new[] {
            new Claim("StaffId", Guid.NewGuid().ToString())
        }));

            var service = new OrderService(mockRepo.Object);
            var result = await service.AddAsync(dto, user);

            Assert.True(result);
            Assert.Equal(9, promotion.Quantity); // Kiểm tra số lượng khuyến mãi đã giảm
        }

        [Fact]
        public async Task AddAsync_PromotionDeleted_ThrowsInvalidOperationException()
        {
            var customerId = Guid.NewGuid();
            var bookId = Guid.NewGuid();
            var promotionId = Guid.NewGuid();

            var customer = new Customer { Id = customerId, IsDeleted = false };
            var book = new Book { Id = bookId, Price = 100, Quantity = 10, IsDeleted = false };

            var promotion = new Promotion
            {
                Id = promotionId,
                Quantity = 10,
                DiscountPercent = 0.1m,
                Condition = 50,
                StartDate = DateTime.Now.AddDays(-1),
                EndDate = DateTime.Now.AddDays(1),
                IsDeleted = true
            };

            var mockRepo = new Mock<IOrderRepository>();
            mockRepo.Setup(r => r.GetCustomerByIdAsync(customerId)).ReturnsAsync(customer);
            mockRepo.Setup(r => r.GetBookByIdAsync(bookId)).ReturnsAsync(book);
            mockRepo.Setup(r => r.GetPromotionByIdAsync(promotionId)).ReturnsAsync(promotion);

            var dto = new OrderCreateDTO
            {
                CustomerId = customerId,
                PromotionId = promotionId,
                Items = new List<OrderItemCreateDTO> { new() { BookId = bookId, Quantity = 1 } }
            };

            var user = new ClaimsPrincipal(new ClaimsIdentity(new[] {
            new Claim("StaffId", Guid.NewGuid().ToString())
        }));

            var service = new OrderService(mockRepo.Object);

            await Assert.ThrowsAsync<InvalidOperationException>(() => service.AddAsync(dto, user));
        }

        [Fact]
        public async Task AddAsync_PromotionNotStarted_ThrowsInvalidOperationException()
        {
            var customerId = Guid.NewGuid();
            var bookId = Guid.NewGuid();
            var promotionId = Guid.NewGuid();

            var customer = new Customer { Id = customerId, IsDeleted = false };
            var book = new Book { Id = bookId, Price = 100, Quantity = 10, IsDeleted = false };

            var promotion = new Promotion
            {
                Id = promotionId,
                Quantity = 10,
                DiscountPercent = 0.1m,
                Condition = 50,
                StartDate = DateTime.Now.AddDays(1),
                EndDate = DateTime.Now.AddDays(2),
                IsDeleted = false
            };

            var mockRepo = new Mock<IOrderRepository>();
            mockRepo.Setup(r => r.GetCustomerByIdAsync(customerId)).ReturnsAsync(customer);
            mockRepo.Setup(r => r.GetBookByIdAsync(bookId)).ReturnsAsync(book);
            mockRepo.Setup(r => r.GetPromotionByIdAsync(promotionId)).ReturnsAsync(promotion);

            var dto = new OrderCreateDTO
            {
                CustomerId = customerId,
                PromotionId = promotionId,
                Items = new List<OrderItemCreateDTO> { new() { BookId = bookId, Quantity = 1 } }
            };

            var user = new ClaimsPrincipal(new ClaimsIdentity(new[] {
            new Claim("StaffId", Guid.NewGuid().ToString())
        }));

            var service = new OrderService(mockRepo.Object);

            await Assert.ThrowsAsync<InvalidOperationException>(() => service.AddAsync(dto, user));
        }

        [Fact]
        public async Task AddAsync_PromotionExpired_ThrowsInvalidOperationException()
        {
            var customerId = Guid.NewGuid();
            var bookId = Guid.NewGuid();
            var promotionId = Guid.NewGuid();

            var customer = new Customer { Id = customerId, IsDeleted = false };
            var book = new Book { Id = bookId, Price = 100, Quantity = 10, IsDeleted = false };

            var promotion = new Promotion
            {
                Id = promotionId,
                Quantity = 10,
                DiscountPercent = 0.1m,
                Condition = 50,
                StartDate = DateTime.Now.AddDays(-3),
                EndDate = DateTime.Now.AddDays(-1),
                IsDeleted = false
            };

            var mockRepo = new Mock<IOrderRepository>();
            mockRepo.Setup(r => r.GetCustomerByIdAsync(customerId)).ReturnsAsync(customer);
            mockRepo.Setup(r => r.GetBookByIdAsync(bookId)).ReturnsAsync(book);
            mockRepo.Setup(r => r.GetPromotionByIdAsync(promotionId)).ReturnsAsync(promotion);

            var dto = new OrderCreateDTO
            {
                CustomerId = customerId,
                PromotionId = promotionId,
                Items = new List<OrderItemCreateDTO> { new() { BookId = bookId, Quantity = 1 } }
            };

            var user = new ClaimsPrincipal(new ClaimsIdentity(new[] {
            new Claim("StaffId", Guid.NewGuid().ToString())
        }));

            var service = new OrderService(mockRepo.Object);

            await Assert.ThrowsAsync<InvalidOperationException>(() => service.AddAsync(dto, user));
        }

        [Fact]
        public async Task AddAsync_PromotionQuantityZero_ThrowsInvalidOperationException()
        {
            var customerId = Guid.NewGuid();
            var bookId = Guid.NewGuid();
            var promotionId = Guid.NewGuid();

            var customer = new Customer { Id = customerId, IsDeleted = false };
            var book = new Book { Id = bookId, Price = 100, Quantity = 10, IsDeleted = false };

            var promotion = new Promotion
            {
                Id = promotionId,
                Quantity = 0,
                DiscountPercent = 0.1m,
                Condition = 50,
                StartDate = DateTime.Now.AddDays(-1),
                EndDate = DateTime.Now.AddDays(1),
                IsDeleted = false
            };

            var mockRepo = new Mock<IOrderRepository>();
            mockRepo.Setup(r => r.GetCustomerByIdAsync(customerId)).ReturnsAsync(customer);
            mockRepo.Setup(r => r.GetBookByIdAsync(bookId)).ReturnsAsync(book);
            mockRepo.Setup(r => r.GetPromotionByIdAsync(promotionId)).ReturnsAsync(promotion);

            var dto = new OrderCreateDTO
            {
                CustomerId = customerId,
                PromotionId = promotionId,
                Items = new List<OrderItemCreateDTO> { new() { BookId = bookId, Quantity = 1 } }
            };

            var user = new ClaimsPrincipal(new ClaimsIdentity(new[] {
            new Claim("StaffId", Guid.NewGuid().ToString())
        }));

            var service = new OrderService(mockRepo.Object);

            await Assert.ThrowsAsync<InvalidOperationException>(() => service.AddAsync(dto, user));
        }

        [Fact]
        public async Task AddAsync_PromotionConditionNotMet_ThrowsInvalidOperationException()
        {
            var customerId = Guid.NewGuid();
            var bookId = Guid.NewGuid();
            var promotionId = Guid.NewGuid();

            var customer = new Customer { Id = customerId, IsDeleted = false };
            var book = new Book { Id = bookId, Price = 40, Quantity = 10, IsDeleted = false };

            var promotion = new Promotion
            {
                Id = promotionId,
                Quantity = 10,
                DiscountPercent = 0.1m,
                Condition = 50,
                StartDate = DateTime.Now.AddDays(-1),
                EndDate = DateTime.Now.AddDays(1),
                IsDeleted = false
            };

            var mockRepo = new Mock<IOrderRepository>();
            mockRepo.Setup(r => r.GetCustomerByIdAsync(customerId)).ReturnsAsync(customer);
            mockRepo.Setup(r => r.GetBookByIdAsync(bookId)).ReturnsAsync(book);
            mockRepo.Setup(r => r.GetPromotionByIdAsync(promotionId)).ReturnsAsync(promotion);

            var dto = new OrderCreateDTO
            {
                CustomerId = customerId,
                PromotionId = promotionId,
                Items = new List<OrderItemCreateDTO> { new() { BookId = bookId, Quantity = 1 } }
            };

            var user = new ClaimsPrincipal(new ClaimsIdentity(new[] {
            new Claim("StaffId", Guid.NewGuid().ToString())
        }));

            var service = new OrderService(mockRepo.Object);

            await Assert.ThrowsAsync<InvalidOperationException>(() => service.AddAsync(dto, user));
        }
    }

}
