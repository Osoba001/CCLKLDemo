namespace CCKLDemo.Database.Models
{
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
}
