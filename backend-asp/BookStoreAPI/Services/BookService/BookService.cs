using BookStoreAPI.Data.Entities;
using BookStoreAPI.Services.AuthorService.Repositories;
using BookStoreAPI.Services.BookService.DTOs;
using BookStoreAPI.Services.BookService.Interfaces;
using BookStoreAPI.Services.BookService.Repositories;
using BookStoreAPI.Services.CategoryService.Repositories;
using BookStoreAPI.Services.PublisherService.Repositories;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace BookStoreAPI.Services.BookService
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IPublisherRepository _publisherRepository;

        public BookService(IBookRepository bookRepository, IAuthorRepository authorRepository, ICategoryRepository categoryRepository, IPublisherRepository publisherRepository)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _categoryRepository = categoryRepository;
            _publisherRepository = publisherRepository;
        }

        public async Task<IEnumerable<BookDetailDTO>> GetAllAsync()
        {
            var books = await _bookRepository.GetAllAsync();

            return books.Select(b => new BookDetailDTO
            {
                Id = b.Id,
                Isbn = b.Isbn,
                Title = b.Title,
                CategoryName = b.Category.Name,
                AuthorName = b.Author.Name,
                PublisherName = b.Publisher.Name,
                CategoryId = b.Category.Id,
                AuthorId = b.Author.Id,
                PublisherId = b.Publisher.Id,
                YearOfPublication = b.YearOfPublication,
                Price = b.Price,
                Image = b.Image,
                Quantity = b.Quantity,
                IsDeleted = b.IsDeleted
            });
        }

        public async Task<BookDTO?> GetByIdAsync(Guid id)
        {
            var book = await _bookRepository.GetByIdAsync(id);

            if (book == null)
                throw new KeyNotFoundException($"Không tìm thấy sách có id '{id}'.");

            return new BookDTO
            {
                Id = book.Id,
                Isbn = book.Isbn,
                Title = book.Title,
                CategoryId = book.CategoryId,
                AuthorId = book.AuthorId,
                PublisherId = book.PublisherId,
                YearOfPublication = book.YearOfPublication,
                Price = book.Price,
                Image = book.Image,
                Quantity = book.Quantity,
                IsDeleted = book.IsDeleted
            };
        }

        public async Task<BookDTO?> GetByIsbnAsync(string isbn)
        {
            var book = await _bookRepository.GetByIsbnAsync(isbn);

            if (book == null)
                throw new KeyNotFoundException($"Không tìm thấy sách có isbn '{isbn}'.");

            return new BookDTO
            {
                Id = book.Id,
                Isbn = book.Isbn,
                Title = book.Title,
                CategoryId = book.CategoryId,
                AuthorId = book.AuthorId,
                PublisherId = book.PublisherId,
                YearOfPublication = book.YearOfPublication,
                Price = book.Price,
                Image = book.Image,
                Quantity = book.Quantity,
                IsDeleted = book.IsDeleted
            };
        }

        public async Task<IEnumerable<BookDetailDTO>> SearchByKeywordAsync(string keyword)
        {
            var books = await _bookRepository.SearchByKeywordAsync(keyword);

            return books.Select(b => new BookDetailDTO
            {
                Id = b.Id,
                Isbn = b.Isbn,
                Title = b.Title,
                CategoryName = b.Category.Name,
                AuthorName = b.Author.Name,
                PublisherName = b.Publisher.Name,
                YearOfPublication = b.YearOfPublication,
                Price = b.Price,
                Image = b.Image,
                Quantity = b.Quantity,
                IsDeleted = b.IsDeleted
            });
        }

        public async Task<bool> AddAsync(BookCreateDTO bookCreateDTO)
        {
            await ValidateForeignKeysAsync(bookCreateDTO);

            if (bookCreateDTO.YearOfPublication <= 0)
                throw new ArgumentException("Năm xuất bản phải lớn hơn 0.");

            if (bookCreateDTO.Price <= 0)
                throw new ArgumentException("Giá phải lớn hơn 0.");

            if (string.IsNullOrWhiteSpace(bookCreateDTO.Image))
                throw new ArgumentException("Hình ảnh không được để trống hoặc null.");

            if (bookCreateDTO.Quantity < 0)
                throw new ArgumentException("Số lượng phải lớn hơn hoặc bằng 0.");

            var existingBook = await _bookRepository.GetByIsbnAsync(bookCreateDTO.Isbn);
            if (existingBook != null)
                throw new InvalidOperationException("Đã có một cuốn sách có cùng mã ISBN.");

            var book = new Book
            {
                Isbn = bookCreateDTO.Isbn,
                Title = bookCreateDTO.Title,
                CategoryId = bookCreateDTO.CategoryId,
                AuthorId = bookCreateDTO.AuthorId,
                PublisherId = bookCreateDTO.PublisherId,
                YearOfPublication = bookCreateDTO.YearOfPublication,
                Price = bookCreateDTO.Price,
                Image = bookCreateDTO.Image,
                Quantity = bookCreateDTO.Quantity
            };

            await _bookRepository.AddAsync(book);
            return await _bookRepository.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(Guid id, BookUpdateDTO bookUpdateDTO)
        {
            var category = await _categoryRepository.GetByIdAsync(bookUpdateDTO.CategoryId);
            if (category == null || category.IsDeleted)
                throw new ArgumentException("Danh mục không hợp lệ hoặc đã bị xóa.");

            var author = await _authorRepository.GetByIdAsync(bookUpdateDTO.AuthorId);
            if (author == null || author.IsDeleted)
                throw new ArgumentException("Tác giả không hợp lệ hoặc đã bị xóa.");

            var publisher = await _publisherRepository.GetByIdAsync(bookUpdateDTO.PublisherId);
            if (publisher == null || publisher.IsDeleted)
                throw new ArgumentException("Nhà xuất bản không hợp lệ hoặc đã bị xóa.");

            var existingBook = await _bookRepository.GetByIdAsync(id);
            if (existingBook == null)
                throw new KeyNotFoundException($"Không tìm thấy sách có id '{id}'.");

            if (bookUpdateDTO.YearOfPublication <= 0)
                throw new ArgumentException("Năm xuất bản phải lớn hơn 0.");

            if (bookUpdateDTO.Price <= 0)
                throw new ArgumentException("Giá phải lớn hơn 0.");

            if (string.IsNullOrWhiteSpace(bookUpdateDTO.Image))
                throw new ArgumentException("Hình ảnh không được để trống hoặc null.");

            if (bookUpdateDTO.Quantity < 0)
                throw new ArgumentException("Số lượng phải lớn hơn hoặc bằng 0.");

            var duplicateBook = await _bookRepository.GetByIsbnAsync(bookUpdateDTO.Isbn);
            if (duplicateBook != null && duplicateBook.Id != id)
                throw new InvalidOperationException("Đã có một cuốn sách có cùng mã ISBN.");

            existingBook.Isbn = bookUpdateDTO.Isbn;
            existingBook.Title = bookUpdateDTO.Title;
            existingBook.CategoryId = bookUpdateDTO.CategoryId;
            existingBook.AuthorId = bookUpdateDTO.AuthorId;
            existingBook.PublisherId = bookUpdateDTO.PublisherId;
            existingBook.YearOfPublication = bookUpdateDTO.YearOfPublication;
            existingBook.Price = bookUpdateDTO.Price;
            existingBook.Image = bookUpdateDTO.Image;
            existingBook.Quantity = bookUpdateDTO.Quantity;
            existingBook.IsDeleted = bookUpdateDTO.IsDeleted;

            _bookRepository.Update(existingBook);
            return await _bookRepository.SaveChangesAsync();
        }


        public async Task<bool> DeleteAsync(Guid id)
        {
            var existingBook = await _bookRepository.GetByIdAsync(id);

            if (existingBook == null)
                throw new KeyNotFoundException($"Không tìm thấy sách có id '{id}'.");

            if (existingBook.IsDeleted == true)
                throw new InvalidOperationException($"Sách có id {id} đã bị xóa.");

            _bookRepository.Delete(existingBook);

            return await _bookRepository.SaveChangesAsync();
        }

        private async Task ValidateForeignKeysAsync(BookCreateDTO dto)
        {
            var category = await _categoryRepository.GetByIdAsync(dto.CategoryId);
            if (category == null || category.IsDeleted)
                throw new ArgumentException("Danh mục không hợp lệ hoặc đã bị xóa.");

            var author = await _authorRepository.GetByIdAsync(dto.AuthorId);
            if (author == null || author.IsDeleted)
                throw new ArgumentException("Tác giả không hợp lệ hoặc đã bị xóa.");

            var publisher = await _publisherRepository.GetByIdAsync(dto.PublisherId);
            if (publisher == null || publisher.IsDeleted)
                throw new ArgumentException("Nhà xuất bản không hợp lệ hoặc đã bị xóa.");
        }
    }
}
