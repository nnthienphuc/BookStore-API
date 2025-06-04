using System.ComponentModel.DataAnnotations;

namespace EnterpriseServiceBus.DTOs.Auth;

public class LoginDTO
{
    [Required(ErrorMessage = "Email is required.")]
    [StringLength(50)]
    [RegularExpression(@"\S+", ErrorMessage = "Email cannot be whitespace")]
    public required string Email { get; set; }
    public required string Password { get; set; }

}
