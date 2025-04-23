namespace BookStoreAPI.Services.AuthService.DTOs
{
    public class RegisterDTO
    {
        public String FamilyName { get; set; }
        public String GivenName { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public String Email { get; set; }
        public String Phone { get; set; }
        public String Address { get; set; }
        public String CitizenIdentification { get; set; }
        public String Password { get; set; }
        public String ConfirmPassword { get; set; }
        public bool Gender { get; set; }    // false = nam, true = nu
        public bool Role { get; set; }      // false = staff, true = admin
    }
}
