using BookStoreAPI.Services.PublisherService.DTOs;
using BookStoreAPI.Data.Entities;
using BookStoreAPI.Services.PublisherService.Repositories;
using BookStoreAPI.Services.PublisherService.Interfaces;

namespace BookStoreAPI.Services.PublisherService
{
    public class PublisherService : IPublisherService
    {
        private readonly IPublisherRepository _publisherRepository;

        public PublisherService(IPublisherRepository publisherRepository)
        {
            _publisherRepository = publisherRepository;
        }

        public async Task<IEnumerable<PublisherDTO>> GetAllAsync()
        {
            var publishers = await _publisherRepository.GetAllAsync();

            return publishers.Select(p => new PublisherDTO
            {
                Id = p.Id,
                Name = p.Name,
                IsDeleted = p.IsDeleted
            });
        }

        public async Task<PublisherDTO?> GetByIdAsync(Guid id)
        {
            var publisher = await _publisherRepository.GetByIdAsync(id);

            if (publisher == null)
                throw new KeyNotFoundException($"Không tìm thấy nhà xuất bản với ID {id}.");

            return new PublisherDTO
            {
                Id = publisher.Id,
                Name = publisher.Name,
                IsDeleted = publisher.IsDeleted
            };
        }

        public async Task<PublisherDTO?> GetByNameAsync(string name)
        {
            var publisher = await _publisherRepository.GetByNameAsync(name);

            if (publisher == null)
                throw new KeyNotFoundException($"Không tìm thấy nhà xuất bản với tên '{name}'.");

            return new PublisherDTO
            {
                Id = publisher.Id,
                Name = publisher.Name,
                IsDeleted = publisher.IsDeleted
            };
        }

        public async Task<IEnumerable<PublisherDTO>> SearchByKeywordAsync(string keyword)
        {
            var publishers = await _publisherRepository.SearchByKeywordAsync(keyword);

            return publishers.Select(p => new PublisherDTO
            {
                Id = p.Id,
                Name = p.Name,
                IsDeleted = p.IsDeleted
            });
        }

        public async Task<bool> AddAsync(PublisherCreateDTO publisherCreateDTO)
        {
            var existingPublisher = await _publisherRepository.GetByNameAsync(publisherCreateDTO.Name);

            if (existingPublisher != null)
                throw new InvalidOperationException("Đã tồn tại nhà xuất bản với tên này.");

            var publisher = new Publisher
            {
                Name = publisherCreateDTO.Name
            };

            await _publisherRepository.AddAsync(publisher);

            return await _publisherRepository.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(Guid id, PublisherUpdateDTO publisherUpdateDTO)
        {
            var existingPublisher = await _publisherRepository.GetByIdAsync(id);

            if (existingPublisher == null)
                throw new KeyNotFoundException($"Không tìm thấy nhà xuất bản với ID {id}.");

            var duplicatePublisher = await _publisherRepository.GetByNameAsync(publisherUpdateDTO.Name);

            if (duplicatePublisher != null && duplicatePublisher.Id != id)
                throw new InvalidOperationException("Đã tồn tại nhà xuất bản khác với cùng tên.");

            existingPublisher.Name = publisherUpdateDTO.Name;
            existingPublisher.IsDeleted = publisherUpdateDTO.IsDeleted;

            _publisherRepository.Update(existingPublisher);

            return await _publisherRepository.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var existingPublisher = await _publisherRepository.GetByIdAsync(id);

            if (existingPublisher == null)
                throw new KeyNotFoundException($"Không tìm thấy nhà xuất bản với ID {id}.");

            if (existingPublisher.IsDeleted == true)
                throw new InvalidOperationException($"Nhà xuất bản với ID {id} đã bị xoá trước đó.");

            _publisherRepository.Delete(existingPublisher);

            return await _publisherRepository.SaveChangesAsync();
        }
    }
}
