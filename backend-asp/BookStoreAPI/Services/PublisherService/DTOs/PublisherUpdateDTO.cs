namespace BookStoreAPI.Services.PublisherService.DTOs
{
    public class PublisherUpdateDTO
    {
        public required string Name { get; set; }
        public required bool IsDeleted { get; set; }
    }
}
