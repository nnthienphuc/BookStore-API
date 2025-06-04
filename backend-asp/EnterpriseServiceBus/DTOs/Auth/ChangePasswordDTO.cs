namespace EnterpriseServiceBus.DTOs.Auth;


public class ChangePasswordDTO
{
    public required String OldPassword { get; set; }
    public required String NewPassword { get; set; }
    public required String ConfirmNewPassword { get; set; }
}
