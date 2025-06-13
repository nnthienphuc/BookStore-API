using System.ComponentModel.DataAnnotations;

namespace BookStoreAPI.Services.PublisherService.DTOs
{
    public class PublisherCreateDTO
    {
        [Required(ErrorMessage = "Tên nhà xuất bản không được để trống.")]
        [StringLength(100)]
        [RegularExpression(@".*\S.*", ErrorMessage = "Tên nhà xuất bản không được chỉ chứa khoảng trắng.")]
        public required string Name { get; set; }
    }
}
