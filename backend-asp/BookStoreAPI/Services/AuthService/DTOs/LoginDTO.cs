using System.ComponentModel.DataAnnotations;

namespace BookStoreAPI.Services.AuthService.DTOs
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Email là bắt buộc.")]
        [StringLength(50)]
        [RegularExpression(@"\S+", ErrorMessage = "Email không được có dấu cách")]
        public required String Email { get; set; }
        public required String Password { get; set; }

    }
}
