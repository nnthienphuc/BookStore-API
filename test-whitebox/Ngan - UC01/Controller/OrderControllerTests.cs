using System.Security.Claims;
using BookStoreAPI.Controllers;
using BookStoreAPI.Data.Entities;
using BookStoreAPI.Services.OrderService.DTOs;
using BookStoreAPI.Services.OrderService.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;

namespace Test.Controller
{
    public class OrderControllerTests
    {
        [Fact]
        public async Task Add_ReturnsOkResult_WithSuccessMessage()
        {
            // Arrange
            var mockService = new Mock<IOrderService>();

            var orderCreateDto = new OrderCreateDTO
            {
                CustomerId = Guid.NewGuid(),
                PromotionId = null,
                Items = new List<OrderItemCreateDTO>
                {
                    new OrderItemCreateDTO { BookId = Guid.NewGuid(), Quantity = 2 },
                    new OrderItemCreateDTO { BookId = Guid.NewGuid(), Quantity = 1 }
                }
            };

            // Giả lập service AddAsync
            mockService
                .Setup(s => s.AddAsync(orderCreateDto, It.IsAny<ClaimsPrincipal>()))
                .ReturnsAsync(true);

            var controller = new OrderController(mockService.Object);

            // Giả lập người dùng
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
            new Claim(ClaimTypes.NameIdentifier, "test-user-id")
            }, "mock"));

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };

            // Act
            var result = await controller.Add(orderCreateDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);

            // Ép kiểu rõ ràng từ anonymous object (dễ test hơn)
            var json = JsonConvert.SerializeObject(okResult.Value);
            var response = JsonConvert.DeserializeObject<MessageResponse>(json);

            Assert.Equal("Đơn hàng tạo thành công.", response.Message);
        }
            public class MessageResponse
        {
            public string Message { get; set; }
        }
    }
}
