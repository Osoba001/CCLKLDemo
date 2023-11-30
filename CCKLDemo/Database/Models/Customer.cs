namespace CCKLDemo.Database.Models
{
    public class Customer: EntityBase
    {
        public string FirstName { get; set; }=Guid.NewGuid().ToString();
        public string LastName { get; set; } = Guid.NewGuid().ToString();
        public string OtherName { get; set; } = Guid.NewGuid().ToString();
        public string Email { get; set; } = Guid.NewGuid().ToString();
        public string Phone { get; set; } = Guid.NewGuid().ToString();
        public string StateName { get; set; } = Guid.NewGuid().ToString();
        public DateTime DOB { get; set; }
        public required Guid CountryId{ get; set; }
        public Country Country { get; set; }
        public string Gender { get; set; } = Guid.NewGuid().ToString();
        public Purchase[] Purchases { get; set; } = Array.Empty<Purchase>();
        public Credit[] Credits { get; set; } = Array.Empty<Credit>();
    }
}
