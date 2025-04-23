namespace BookStoreAPI.Services.AuthService.DTOs
{
    public class ChangePasswordDTO
    {
        public String OldPassword { get; set; }
        public String NewPassword { get; set; }
        public String ConfirmNewPassword { get; set; }
    }
}
