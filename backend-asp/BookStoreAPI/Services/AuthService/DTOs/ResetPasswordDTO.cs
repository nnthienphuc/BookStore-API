using System.ComponentModel.DataAnnotations;

namespace BookStoreAPI.Services.AuthService.DTOs
{
    public class ResetPasswordDTO
    {
        [Required(ErrorMessage = "Email là bắt buộc.")]
        [StringLength(50)]
        [RegularExpression(@"\S+", ErrorMessage = "Email không được để khoảng trắng.")]
        public required string Email { get; set; }
    }
}
