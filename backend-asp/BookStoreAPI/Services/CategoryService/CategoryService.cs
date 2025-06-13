using BookStoreAPI.Services.CategoryService.DTOs;
using BookStoreAPI.Data.Entities;
using BookStoreAPI.Services.CategoryService.Interfaces;
using BookStoreAPI.Services.CategoryService.Repositories;

namespace BookStoreAPI.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService (ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<CategoryDTO>> GetAllAsync ()
        {
            var categories = await _categoryRepository.GetAllAsync();

            return categories.Select(c => new CategoryDTO
            {
                Id = c.Id,
                Name = c.Name,
                IsDeleted = c.IsDeleted
            });
        }

        public async Task<CategoryDTO?> GetByIdAsync (Guid id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);

            if (category == null)
                throw new KeyNotFoundException($"Không tìm thấy danh mục có id '{id}'.");

            return new CategoryDTO
            {
                Id = category.Id,
                Name = category.Name,
                IsDeleted = category.IsDeleted
            };
        }

        public async Task<CategoryDTO?> GetByNameAsync (string name)
        {
            var category = await _categoryRepository.GetByNameAsync(name);

            if (category == null)
                throw new KeyNotFoundException($"Không tìm thấy danh mục có tên '{name}'.");

            return new CategoryDTO
            {
                Id = category.Id,
                Name = category.Name,
                IsDeleted = category.IsDeleted
            };
        }

        public async Task<IEnumerable<CategoryDTO>> SearchByKeywordAsync (string keyword)
        {
            var categories = await _categoryRepository.SearchByKeywordAsync(keyword);

            return categories.Select(c => new CategoryDTO
            {
                Id = c.Id,
                Name = c.Name,
                IsDeleted = c.IsDeleted
            });
        }

        public async Task<bool> AddAsync (CategoryCreateDTO categoryCreateDTO)
        {
            var existingCategory = await _categoryRepository.GetByNameAsync(categoryCreateDTO.Name);

            if (existingCategory != null)
            {
                throw new InvalidOperationException("Một danh mục có cùng tên đã tồn tại.");
            }

            var category = new Category
            {
                Name = categoryCreateDTO.Name
            };

            await _categoryRepository.AddAsync(category);

            return await _categoryRepository.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(Guid id, CategoryUpdateDTO categoryUpdateDTO)
        {
            var existingCategory = await _categoryRepository.GetByIdAsync(id);

            if (existingCategory == null)
                throw new KeyNotFoundException($"Không tìm thấy danh mục có id '{id}'.");

            var checkCategory = await _categoryRepository.GetByNameAsync(categoryUpdateDTO.Name);
            if ((checkCategory != null) && (existingCategory.Id != checkCategory.Id))  // tranh tinh trang Name trung id (truong hop chi thay doi IsDeleted)
                throw new InvalidOperationException("Đã tồn tại một danh mục có cùng tên.");

            existingCategory.Name = categoryUpdateDTO.Name;
            existingCategory.IsDeleted = categoryUpdateDTO.IsDeleted;

            _categoryRepository.Update(existingCategory);

            return await _categoryRepository.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync (Guid id)
        {
            var existingCategory = await _categoryRepository.GetByIdAsync(id);

            if (existingCategory == null)
                throw new KeyNotFoundException($"Không tìm thấy danh mục có id '{id}'.");

            if (existingCategory.IsDeleted)
                throw new InvalidOperationException($"Danh mục có id {id} đã bị xóa.");

            _categoryRepository.Delete(existingCategory);

            return await _categoryRepository.SaveChangesAsync();
        }
    }
}
