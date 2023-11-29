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
    public class Market : EntityBase
    {
        public string Name { get; set; } = Guid.NewGuid().ToString();
        public string MarketType { get; set; } = Guid.NewGuid().ToString();
        public string Email { get; set; } = Guid.NewGuid().ToString();
        public string Phone { get; set; } = Guid.NewGuid().ToString();
        public string OwnerName { get; set; } = Guid.NewGuid().ToString();
        public required Guid CountryId { get; set; }
        public Country Country { get; set; }
        public Purchase[] purchases { get; set; }= Array.Empty<Purchase>();
    }
    public class Country : EntityBase
    {
        public string Name { get; set; } = Guid.NewGuid().ToString();
        public string Currency { get; set; } = Guid.NewGuid().ToString();
        public string Continent { get; set; } = Guid.NewGuid().ToString();
        public int PopulationSize { get; set; }
        public Market[] Markets { get; set; } = Array.Empty<Market>();
        public Customer[] Owners { get; set; } = Array.Empty<Customer>();
    }
    
    public class Credit : EntityBase
    {
        public required decimal Amount { get; set; }
        public required Guid CustomerId { get; set; }
        public Customer Customer { get; set; }
        public required string From { get; set; } = Guid.NewGuid().ToString();
        public bool Success { get; set; }=true;
    }

    public class Purchase : EntityBase
    {
        public required decimal Amount { get; set; }
        public required Guid CustomerId { get; set; }
        public Customer Customer { get; set; }
        public required Guid MarketId { get; set; }
        public Market Market { get; set; }
        public bool Success { get; set; } = true;
    }
}
