namespace BookStoreAPI.Services.CustomerSevice.DTOs
{
    public class CustomerUpdateDTO
    {
        public required string FamilyName { get; set; }
        public required string GivenName { get; set; }
        public required DateOnly DateOfBirth { get; set; }
        public required string Address { get; set; }
        public required string Phone { get; set; }
        public required bool Gender { get; set; }
        public required bool IsDeleted { get; set; }
    }
}
