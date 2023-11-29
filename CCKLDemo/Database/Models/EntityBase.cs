namespace CCKLDemo.Database.Models
{
    public class EntityBase
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }= DateTime.UtcNow;
        public DateTime LastModifyDate { get; set; }= DateTime.UtcNow;
    }
}
