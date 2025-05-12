namespace BookStoreAPI.Services.StaffService.DTOs
{
    public class StaffUpdateDTO
    {
        public required string FamilyName { get; set; }
        public required string GivenName { get; set; }
        public required DateOnly DateOfBirth { get; set; }
        public required string Address { get; set; }
        public required string Phone { get; set; }
        public required string Email { get; set; }
        public required string CitizenIdentification { get; set; }
        public required bool Role { get; set; }
        public required bool Gender { get; set; }
        public required bool IsActived { get; set; }
        public required bool IsDeleted { get; set; }
    }
}
