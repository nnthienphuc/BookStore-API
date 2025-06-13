using System.ComponentModel.DataAnnotations;

namespace BookStoreAPI.Services.BookService.DTOs
{
    public class BookCreateDTO
    {
        [Required(ErrorMessage = "Mã ISBN là bắt buộc.")]
        [StringLength(13)]
        [RegularExpression(@"\S+", ErrorMessage = "ISBN không được để khoảng trắng.")]
        public required string Isbn { get; set; }

        [Required(ErrorMessage = "Tiêu đề là bắt buộc.")]
        [StringLength(100)]
        [RegularExpression(@".*\S.*", ErrorMessage = "Tên không được để khoảng trắng.")]
        public required string Title { get; set; }
        public required Guid CategoryId { get; set; }
        public required Guid AuthorId { get; set; }
        public required Guid PublisherId { get; set; }
        public required short YearOfPublication { get; set; }
        public required decimal Price { get; set; }
        public required string Image { get; set; }
        public required int Quantity { get; set; }
    }
}
