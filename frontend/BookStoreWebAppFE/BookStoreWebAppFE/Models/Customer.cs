namespace BookStoreWebAppFE.Models
{
    public class Customer
    {
        public Guid id { get; set; }

        public string familyName { get; set; } = null!;

        public string givenName { get; set; } = null!;
        public string FullName { get { return $"{familyName} {givenName}"; } }
        public string address { get; set; } = null!;
        public string phone { get; set; } = null!;
        public bool gender { get; set; } = true;
        public DateOnly dateOfBirth { get; set; }
        public bool IsDeleted { get; set; } = false;

    }
}
