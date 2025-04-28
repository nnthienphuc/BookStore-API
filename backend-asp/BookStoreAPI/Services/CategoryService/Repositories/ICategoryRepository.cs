using BookStoreAPI.Services.CategoryService.Entities;

namespace BookStoreAPI.Services.CategoryService.Repositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category?> GetByIdAsync(Guid id);
        Task<Category?> GetByNameAsync(string name);
        Task AddAsync(Category category);
        void Update(Category category);
        void Delete(Category category);              // soft delete -> gan IsDeleted = true
        Task<bool> SaveChangesAsync();
    }
}
