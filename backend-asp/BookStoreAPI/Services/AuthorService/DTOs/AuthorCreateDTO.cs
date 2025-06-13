using System.ComponentModel.DataAnnotations;

namespace BookStoreAPI.Services.AuthorService.DTOs
{
    public class AuthorCreateDTO
    {
        [Required(ErrorMessage = "Tên là bắt buộc.")]
        [StringLength(100)]
        [RegularExpression(@".*\S.*", ErrorMessage = "Tên không được để trống.")]
        public required string Name { get; set; }
    }
}
