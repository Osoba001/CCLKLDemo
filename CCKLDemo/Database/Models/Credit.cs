namespace CCKLDemo.Database.Models
{
    public class Credit : EntityBase
    {
        public required decimal Amount { get; set; }
        public required Guid CustomerId { get; set; }
        public Customer Customer { get; set; }
        public string From { get; set; } = Guid.NewGuid().ToString();
        public bool Success { get; set; }=true;
    }
}
