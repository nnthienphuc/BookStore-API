using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookStoreWebAppFE.Models
{
    public  class Staff
    {
       
        public Guid id { get; set; }

        [Required]
        public string FamilyName { get; set; }
        [Required]
        public string GivenName { get; set; }
        public string FullName { get { return $"{FamilyName} {GivenName}";} }
        public DateOnly DateOfBirth { get; set; }
        [Required]
        public string Address { get; set; }

        [Required, MaxLength(10)]
        public string Phone { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string CitizenIdentification { get; set; }
        public bool Gender { get; set; }
        public  bool Role { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsActived { get; set; }
        public String Password { get; set; }
        public String ConfirmPassword { get; set; }

    }
}
