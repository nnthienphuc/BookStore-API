
namespace EnterpriseServiceBus.Entities;

public partial class Staff : BaseEntity
{

    public string FamilyName { get; set; } = null!;

    public string GivenName { get; set; } = null!;

    public DateOnly DateOfBirth { get; set; }

    public string Address { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string CitizenIdentification { get; set; } = null!;

    public string HashPassword { get; set; } = null!;

    public bool Role { get; set; }

    public bool Gender { get; set; }

    public bool IsActived { get; set; }

}
