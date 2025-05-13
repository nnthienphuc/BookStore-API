using BookStoreAPI.Data.Entities;
using BookStoreAPI.Services.BookService.DTOs;
using BookStoreAPI.Services.BookService.Interfaces;
using BookStoreAPI.Services.BookService.Repositories;

namespace BookStoreAPI.Services.BookService
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
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
                throw new KeyNotFoundException($"Book with id '{id}' not found.");

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
                throw new KeyNotFoundException($"Book with isbn '{isbn}' not found.");

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
            if (string.IsNullOrWhiteSpace(keyword))
                throw new ArgumentException("Keyword cannot be null or empty.");

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
            if (string.IsNullOrWhiteSpace(bookCreateDTO.Isbn))
                throw new ArgumentException("ISBN cannot be null or empty.");

            if (string.IsNullOrWhiteSpace(bookCreateDTO.Title))
                throw new ArgumentException("Title cannot be null or empty.");

            if (bookCreateDTO.YearOfPublication <= 0)
                throw new ArgumentException("Year of publication must be greater than 0.");

            if (bookCreateDTO.Price <= 0)
                throw new ArgumentException("Price must be greater than 0.");

            if (string.IsNullOrWhiteSpace(bookCreateDTO.Image))
                throw new ArgumentException("Image cannot be null or empty.");

            if (bookCreateDTO.Quantity < 0)
                throw new ArgumentException("Quantity must be 0 or greater.");

            var existingBook = await _bookRepository.GetByIsbnAsync(bookCreateDTO.Isbn);
            if (existingBook != null)
                throw new InvalidOperationException("A book with the same ISBN already exists.");

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
            var existingBook = await _bookRepository.GetByIdAsync(id);
            if (existingBook == null)
                throw new KeyNotFoundException($"Book with id '{id}' not found.");

            if (string.IsNullOrWhiteSpace(bookUpdateDTO.Isbn))
                throw new ArgumentException("ISBN cannot be null or empty.");

            if (string.IsNullOrWhiteSpace(bookUpdateDTO.Title))
                throw new ArgumentException("Title cannot be null or empty.");

            if (bookUpdateDTO.YearOfPublication <= 0)
                throw new ArgumentException("Year of publication must be greater than 0.");

            if (bookUpdateDTO.Price <= 0)
                throw new ArgumentException("Price must be greater than 0.");

            if (string.IsNullOrWhiteSpace(bookUpdateDTO.Image))
                throw new ArgumentException("Image cannot be null or empty.");

            if (bookUpdateDTO.Quantity < 0)
                throw new ArgumentException("Quantity must be 0 or greater.");

            var duplicateBook = await _bookRepository.GetByIsbnAsync(bookUpdateDTO.Isbn);
            if (duplicateBook != null && duplicateBook.Id != id)
                throw new InvalidOperationException("A book with the same ISBN already exists.");

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
                throw new KeyNotFoundException($"Book with id '{id}' not found.");

            if (existingBook.IsDeleted == true)
                throw new InvalidOperationException($"Book with id {id} is already deleted.");

            _bookRepository.Delete(existingBook);

            return await _bookRepository.SaveChangesAsync();
        }
    }
}
