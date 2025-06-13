using System.ComponentModel.DataAnnotations;

namespace BookStoreAPI.Services.AuthService.DTOs
{
    public class RegisterDTO
    {
        [Required(ErrorMessage = "Họ là bắt buộc.")]
        [StringLength(70)]
        [RegularExpression(@".*\S.*", ErrorMessage = "Họ không được để toàn khoảng trắng.")]
        public required string FamilyName { get; set; }

        [Required(ErrorMessage = "Tên là bắt buộc.")]
        [StringLength(30)]
        [RegularExpression(@".*\S.*", ErrorMessage = "Tên không được để toàn khoảng trắng.")]
        public required string GivenName { get; set; }

        public required DateOnly DateOfBirth { get; set; }

        [Required(ErrorMessage = "Địa chỉ là bắt buộc.")]
        [StringLength(50)]
        [RegularExpression(@".*\S.*", ErrorMessage = "Địa chỉ không được để toàn khoảng trắng.")]
        public required string Address { get; set; }

        [Required(ErrorMessage = "Số điện thoại là bắt buộc.")]
        [StringLength(10)]
        [RegularExpression(@"\S+", ErrorMessage = "Số điện thoại không được để khoảng trắng.")]
        public required string Phone { get; set; }

        [Required(ErrorMessage = "Email là bắt buộc.")]
        [StringLength(50)]
        [RegularExpression(@"\S+", ErrorMessage = "Email không được để khoảng trắng.")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Số CCCD là bắt buộc.")]
        [StringLength(12)]
        [RegularExpression(@"\S+", ErrorMessage = "Số CCCD không được để khoảng trắng.")]
        public required string CitizenIdentification { get; set; }

        public required bool Role { get; set; }
        public required bool Gender { get; set; }
        public required string Password { get; set; }
        public required string ConfirmPassword { get; set; }
    }
}
