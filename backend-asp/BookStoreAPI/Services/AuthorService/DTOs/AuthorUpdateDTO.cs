using System.ComponentModel.DataAnnotations;

namespace BookStoreAPI.Services.AuthorService.DTOs
{
    public class AuthorUpdateDTO
    {
        [Required(ErrorMessage = "Tên là bắt buộc.")]
        [StringLength(100)]
        [RegularExpression(@".*\S.*", ErrorMessage = "Tên không được để trống")]
        public required string Name { get; set; }

        public required bool IsDeleted { get; set; }
    }
}
