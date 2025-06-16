using System.ComponentModel.DataAnnotations;
using BookStoreWebAppFE.Components.Helper;

namespace BookStoreWebAppFE.Models
{
    public  class Book
    {
        public Guid id { get; set; }

        [Required(ErrorMessage = "Mã ISBN là bắt buộc.")]
        [StringLength(13, MinimumLength = 13, ErrorMessage = "Phải nhập 13 ký tự!")]
        public string Isbn { get; set; } = null!;
        [Required(ErrorMessage = "Tiêu đề là bắt buộc.")]
        [StringLength(200)]
        public string Title { get; set; }
        [GuidNotEmpty(ErrorMessage = "Thể loại là bắt buộc.")]
        public Guid categoryId { get; set; }
        [GuidNotEmpty(ErrorMessage = "Tác giả là bắt buộc.")]
        public Guid authorId { get; set; }
        [GuidNotEmpty(ErrorMessage = "Nhà xuất bản là bắt buộc.")]
        public Guid publisherId { get; set; }
        [Required(ErrorMessage = "Năm xuất bản là bắt buộc.")]
        public int YearOfPublication { get; set; }
        [Required(ErrorMessage = "Số lượng là bắt buộc.")]
        public int Quantity { get; set; }
        [Required(ErrorMessage = "Hình ảnh là bắt buộc.")]
        public string Image { get; set; }
        [Required(ErrorMessage = "Giá là bắt buộc.")]
        public decimal Price { get; set; }
        //[Required(ErrorMessage =  "")]
        public bool isDeleted { get; set; }

    }
}
