using BookStoreAPI.Data.Entities;
using BookStoreAPI.Services.AuthorService.DTOs;
using BookStoreAPI.Services.AuthorService.Interfaces;
using BookStoreAPI.Services.AuthorService.Repositories;

namespace BookStoreAPI.Services.AuthorService
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<IEnumerable<AuthorDTO>> GetAllAsync ()
        {
            var authors = await _authorRepository.GetAllAsync();

            return authors.Select(a => new AuthorDTO
            {
                Id = a.Id,
                Name = a.Name,
                IsDeleted = a.IsDeleted
            });
        }

        public async Task<AuthorDTO?> GetByIdAsync(Guid id)
        {
            var author = await _authorRepository.GetByIdAsync(id);

            if (author == null)
                throw new KeyNotFoundException($"Không tìm thấy tác giả có id {id}.");

            return new AuthorDTO
            {
                Id = author.Id,
                Name = author.Name,
                IsDeleted = author.IsDeleted
            };
        }

        public async Task<IEnumerable<AuthorDTO>> SearchByKeywordAsync(string keyword)
        {
            var authors = await _authorRepository.SearchByKeywordAsync(keyword);

            return authors.Select(a => new AuthorDTO
            {
                Id = a.Id,
                Name = a.Name,
                IsDeleted = a.IsDeleted
            });
        }

        public async Task<bool> AddAsync (AuthorCreateDTO authorCreateDTO)
        {
            var author = new Author
            {
                Name = authorCreateDTO.Name
            };

            await _authorRepository.AddAsync(author);

            return await _authorRepository.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync (Guid id, AuthorUpdateDTO authorUpdateDTO)
        {
            var author = await _authorRepository.GetByIdAsync(id);

            if (author == null)
                throw new KeyNotFoundException($"Không tìm thấy tác giả có id {id}.");

            author.Name = authorUpdateDTO.Name;
            author.IsDeleted = authorUpdateDTO.IsDeleted;

            _authorRepository.Update(author);

            return await _authorRepository.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var author = await _authorRepository.GetByIdAsync(id);

            if (author == null)
                throw new KeyNotFoundException($"Không tìm thấy tác giả có id {id}.");

            if (author.IsDeleted)
                throw new InvalidOperationException($"Tác giả có id {id} đã bị xóa.");

            _authorRepository.Delete(author);

            return await _authorRepository.SaveChangesAsync();
        }
    }
}
