using BookStoreAPI.Common.Controllers;
using BookStoreAPI.Data.Entities;
using BookStoreAPI.Services.BookService.DTOs;
using BookStoreAPI.Services.BookService.Interfaces;
using BookStoreAPI.Services.OrderService.DTOs;
using BookStoreAPI.Services.PromotionService;
using BookStoreAPI.Services.PromotionService.DTOs;
using BookStoreAPI.Services.PromotionService.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace BookStoreAPI.Controllers
{
    public class BookController : BaseController
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _bookService.GetAllAsync();

            return Ok(result);
        }

        [HttpPost("[Action]")]
        public async Task<IActionResult> UpdateOrder([FromBody] List<OrderItemCreateDTO> items)
        {
            List<BookDTO> books = new();
            foreach (var item in items) {
                var book = await _bookService.GetByIdAsync(item.BookId);
                if (book == null || book.IsDeleted == true)
                {
                    return BadRequest(new { message = "The book does not exist." });
                }

                if (book.Quantity <= item.Quantity)
                {
                    return BadRequest(new { message = "The book does not have enough quantity." });
                }
                book.Quantity = book.Quantity- item.Quantity;
                books.Add(book);
                item.Price = book.Price;
            }
            
            
            try
            {
                foreach(var book in books)
                {
                    BookUpdateDTO updateBook = new BookUpdateDTO
                    {
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
                    await _bookService.UpdateAsync(book.Id, updateBook);
                }
                
                return Ok(items);
            }
            catch (Exception ex) {
                return BadRequest(new { message = "Error when update." });
            }
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchByKeyword([FromQuery] string? keyword)
        {
            var result = await _bookService.SearchByKeywordAsync(keyword);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] BookCreateDTO bookCreateDTO)
        {
            var result = await _bookService.AddAsync(bookCreateDTO);

            return Ok(new { message = "Book added successfully." });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] BookUpdateDTO bookUpdateDTO)
        {
            var result = await _bookService.UpdateAsync(id, bookUpdateDTO);

            return Ok(new { message = "Book updated successfully." });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _bookService.DeleteAsync(id);

            return Ok(new { message = "Book soft deleted successfully." });
        }
    }
}
