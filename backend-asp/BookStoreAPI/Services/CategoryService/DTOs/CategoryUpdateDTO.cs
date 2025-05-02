namespace BookStoreAPI.Services.CategoryService.DTOs
{
    public class CategoryUpdateDTO
    {
        public string Name { get; set; } = null;

        public bool IsDeleted { get; set; } = false;
    }
}
