using System.ComponentModel.DataAnnotations;

namespace BookStoreAPI.Services.CategoryService.DTOs
{
    public class CategoryUpdateDTO
    {
        [Required(ErrorMessage = "Tên là bắt buộc.")]
        [StringLength(100)]
        [RegularExpression(@".*\S.*", ErrorMessage = "Tên không được có khoảng trắng.")]
        public required string Name { get; set; }
        public required bool IsDeleted { get; set; }
    }
}
