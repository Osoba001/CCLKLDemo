namespace CCKLDemo.Database.Models
{
    public class EntityBase
    {
        public Guid Id { get; set; }= Guid.NewGuid();
        public DateTime CreatedDate { get; set; }= DateTime.UtcNow;
        public DateTime LastModifyDate { get; set; }= DateTime.UtcNow;
    }
}
