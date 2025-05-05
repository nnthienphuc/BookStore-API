namespace BookStoreAPI.Services.AuthorService.DTOs
{
    public class AuthorUpdateDTO
    {
        public required string Name { get; set; }

        public required bool IsDeleted { get; set; }
    }
}
