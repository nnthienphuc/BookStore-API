using BookStoreAPI.Services.CategoryService.DTOs;
using BookStoreAPI.Services.CategoryService.Entities;

namespace BookStoreAPI.Services.CategoryService.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDTO>> GetAllAsync();
        Task<CategoryDTO?> GetByIdAsync(Guid id);
        Task<CategoryDTO?> GetByNameAsync(string name);
        Task<bool> AddAsync(CategoryCreateDTO categoryCreateDTO);
        Task<bool> UpdateAsync(CategoryUpdateDTO categoryUpdateDTO);
        Task<bool> DeleteAsync(Guid id); // soft delete -> gan IsDeleted = true
    }
}
