namespace BookStoreAPI.Services.OrderService.DTOs
{
    public class OrderDTO
    {
        public Guid Id { get; set; }
        public string StaffName { get; set; } = null!;
        public Guid StaffId { get; set; }
        public string CustomerName { get; set; } = null!;
        public Guid CustomerId { get; set; } 
        public string CustomerPhone { get; set; } = null!;
        public string? PromotionName { get; set; }
        public Guid PromotionId { get; set; }
        public DateTime CreatedTime { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal SubTotalAmount { get; set; }
        public decimal PromotionAmount { get; set; }
        public bool Status { get; set; }
        public string? Note { get; set; }
        public bool IsDeleted { get; set; }
    }
}
