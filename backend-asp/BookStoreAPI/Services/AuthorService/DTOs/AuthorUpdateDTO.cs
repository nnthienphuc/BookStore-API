namespace BookStoreAPI.Services.AuthorService.DTOs
{
    public class AuthorUpdateDTO
    {
        public string Name { get; set; } = null;

        public bool IsDeleted { get; set; } = false;
    }
}
