
namespace EnterpriseServiceBus.Entities;



public partial class Customer : BaseEntity
{

    public string FamilyName { get; set; } = null!;


    public string GivenName { get; set; } = null!;


    public DateOnly DateOfBirth { get; set; }


    public string Address { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public bool Gender { get; set; }

}
