namespace BookStoreAPI.Services.PromotionService.DTOs
{
    public class PromotionCreateDTO
    {
        public required string Name { get; set; }
        public required DateTime StartDate { get; set; }
        public required DateTime EndDate { get; set; }
        public required decimal Condition { get; set; }
        public required decimal DiscountPercent { get; set; }
        public required short Quantity { get; set; }
    }
}
