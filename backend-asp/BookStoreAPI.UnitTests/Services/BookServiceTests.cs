
using System;
using System.Threading.Tasks;
using Xunit;
using Moq;
using BookStoreAPI.Services.BookService;
using BookStoreAPI.Services.BookService.DTOs;
using BookStoreAPI.Data.Entities;
using BookStoreAPI.Services.BookService.Repositories;
using BookStoreAPI.Services.AuthorService.Repositories;
using BookStoreAPI.Services.CategoryService.Repositories;
using BookStoreAPI.Services.PublisherService.Repositories;

namespace BookStoreAPI.UnitTests.Services
{
    public class BookServiceTests
    {
        private readonly Mock<IBookRepository> _bookRepoMock = new();
        private readonly Mock<IAuthorRepository> _authorRepoMock = new();
        private readonly Mock<ICategoryRepository> _categoryRepoMock = new();
        private readonly Mock<IPublisherRepository> _publisherRepoMock = new();
        private readonly BookService _bookService;

        public BookServiceTests()
        {
            _bookService = new BookService(
                _bookRepoMock.Object,
                _authorRepoMock.Object,
                _categoryRepoMock.Object,
                _publisherRepoMock.Object);
        }

        private BookCreateDTO GetValidBookDto(string isbn = "8935244884708", short year = 2023, decimal price = 150000, string image = "https://cdn1.fahasa.com/media/catalog/product/d/o/doraemon-truyen-dai---nobita-va-lau-dai-duoi-day-bien---tap-4---tb-2023.jpg", int quantity = 10)
        {
            return new BookCreateDTO
            {
                Isbn = isbn,
                Title = "Doraemon - Truyện Dài - Tập 4 - Nobita Và Lâu Đài Dưới Đáy Biển",
                CategoryId = Guid.Parse("35B44E59-DAF3-4561-8A4F-EFA0FF9F629E"),
                AuthorId = Guid.Parse("EAE1CB0D-2104-4F68-884D-562675B04F1A"),
                PublisherId = Guid.Parse("79994AE3-D421-4B92-B895-323C19E12DB9"),
                YearOfPublication = year,
                Price = price,
                Image = image,
                Quantity = quantity
            };
        }

        private void SetupValidForeignKeys(BookCreateDTO dto)
        {
            _categoryRepoMock.Setup(x => x.GetByIdAsync(dto.CategoryId)).ReturnsAsync(new Category { Id = dto.CategoryId });
            _authorRepoMock.Setup(x => x.GetByIdAsync(dto.AuthorId)).ReturnsAsync(new Author { Id = dto.AuthorId });
            _publisherRepoMock.Setup(x => x.GetByIdAsync(dto.PublisherId)).ReturnsAsync(new Publisher { Id = dto.PublisherId });
        }

        [Fact]
        public async Task TC1_AddValidBook_ReturnsTrue()
        {
            var dto = GetValidBookDto();
            SetupValidForeignKeys(dto);
            _bookRepoMock.Setup(x => x.GetByIsbnAsync(dto.Isbn)).ReturnsAsync((Book)null);
            _bookRepoMock.Setup(x => x.AddAsync(It.IsAny<Book>())).Returns(Task.CompletedTask);
            _bookRepoMock.Setup(x => x.SaveChangesAsync()).ReturnsAsync(true);

            var result = await _bookService.AddAsync(dto);
            Assert.True(result);
        }

        [Fact]
        public async Task TC2_EmptyISBN_ThrowsValidationError()
        {
            var dto = GetValidBookDto(isbn: "");
            SetupValidForeignKeys(dto);
            await Assert.ThrowsAsync<ArgumentException>(() => _bookService.AddAsync(dto));
        }

        [Fact]
        public async Task TC3_DuplicateISBN_ThrowsInvalidOperation()
        {
            var dto = GetValidBookDto(isbn: "8935212366533");
            SetupValidForeignKeys(dto);
            _bookRepoMock.Setup(x => x.GetByIsbnAsync(dto.Isbn)).ReturnsAsync(new Book { Id = Guid.NewGuid() });
            await Assert.ThrowsAsync<InvalidOperationException>(() => _bookService.AddAsync(dto));
        }

        [Fact]
        public async Task TC4_EmptyTitle_ThrowsValidationError()
        {
            var dto = GetValidBookDto();
            dto.Title = "";
            SetupValidForeignKeys(dto);
            await Assert.ThrowsAsync<ArgumentException>(() => _bookService.AddAsync(dto));
        }

        [Fact]
        public async Task TC5_WhitespaceTitle_ThrowsValidationError()
        {
            var dto = GetValidBookDto();
            dto.Title = "     ";
            SetupValidForeignKeys(dto);
            await Assert.ThrowsAsync<ArgumentException>(() => _bookService.AddAsync(dto));
        }

        [Fact]
        public async Task TC6_YearLessThan1500_ThrowsError()
        {
            var dto = GetValidBookDto(year: 1499);
            SetupValidForeignKeys(dto);
            await Assert.ThrowsAsync<ArgumentException>(() => _bookService.AddAsync(dto));
        }

        [Fact]
        public async Task TC7_PriceLessThan1000_ThrowsError()
        {
            var dto = GetValidBookDto(price: 500);
            SetupValidForeignKeys(dto);
            await Assert.ThrowsAsync<ArgumentException>(() => _bookService.AddAsync(dto));
        }

        [Fact]
        public async Task TC8_EmptyImage_ThrowsError()
        {
            var dto = GetValidBookDto(image: "");
            SetupValidForeignKeys(dto);
            await Assert.ThrowsAsync<ArgumentException>(() => _bookService.AddAsync(dto));
        }

        [Fact]
        public async Task TC9_InvalidCategory_ThrowsError()
        {
            var dto = GetValidBookDto();
            _categoryRepoMock.Setup(x => x.GetByIdAsync(dto.CategoryId)).ReturnsAsync((Category)null);
            _authorRepoMock.Setup(x => x.GetByIdAsync(dto.AuthorId)).ReturnsAsync(new Author());
            _publisherRepoMock.Setup(x => x.GetByIdAsync(dto.PublisherId)).ReturnsAsync(new Publisher());
            await Assert.ThrowsAsync<ArgumentException>(() => _bookService.AddAsync(dto));
        }

        [Fact]
        public async Task TC10_InvalidAuthor_ThrowsError()
        {
            var dto = GetValidBookDto();
            _categoryRepoMock.Setup(x => x.GetByIdAsync(dto.CategoryId)).ReturnsAsync(new Category());
            _authorRepoMock.Setup(x => x.GetByIdAsync(dto.AuthorId)).ReturnsAsync((Author)null);
            _publisherRepoMock.Setup(x => x.GetByIdAsync(dto.PublisherId)).ReturnsAsync(new Publisher());
            await Assert.ThrowsAsync<ArgumentException>(() => _bookService.AddAsync(dto));
        }

        [Fact]
        public async Task TC11_InvalidPublisher_ThrowsError()
        {
            var dto = GetValidBookDto();
            _categoryRepoMock.Setup(x => x.GetByIdAsync(dto.CategoryId)).ReturnsAsync(new Category());
            _authorRepoMock.Setup(x => x.GetByIdAsync(dto.AuthorId)).ReturnsAsync(new Author());
            _publisherRepoMock.Setup(x => x.GetByIdAsync(dto.PublisherId)).ReturnsAsync((Publisher)null);
            await Assert.ThrowsAsync<ArgumentException>(() => _bookService.AddAsync(dto));
        }
    }
}
