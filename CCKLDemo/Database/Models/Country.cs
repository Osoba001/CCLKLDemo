namespace CCKLDemo.Database.Models
{
    public class Country : EntityBase
    {
        public string Name { get; set; } = Guid.NewGuid().ToString();
        public string Currency { get; set; } = Guid.NewGuid().ToString();
        public string Continent { get; set; } = Guid.NewGuid().ToString();
        public int PopulationSize { get; set; }
        public Market[] Markets { get; set; } = Array.Empty<Market>();
        public Customer[] Owners { get; set; } = Array.Empty<Customer>();
    }
}
