namespace OrderServiceAPI.Services.OrderService.DTOs
{
    public class OrderCreateDTO
    {
        public required Guid CustomerId { get; set; }
        public Guid? PromotionId { get; set; }
        public decimal DiscountPercent  { get; set; }
        public List<OrderItemCreateDTO> Items { get; set; } = new();
    }
}
