using BookStoreAPI.Services.CategoryService.DTOs;
using BookStoreAPI.Services.CategoryService.Interfaces;
using BookStoreAPI.Services.CategoryService.Repositories;

namespace BookStoreAPI.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<CategoryDTO>> GetAllSync()
        {
            var categories = await _categoryRepository.GetAllAsync();

            return categories.Select(c => new CategoryDTO
            {
                Id = c.Id,
                Name = c.Name
            });
        }

        public async Task<CategoryDTO?> GetByIdAsync(Guid id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
        }
    }
}
