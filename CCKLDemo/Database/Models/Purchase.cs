using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CCKLDemo.Database.Models
{
    public class Purchase : EntityBase
    {
        public required decimal Amount { get; set; }
        public required Guid CustomerId { get; set; }
        public Customer Customer { get; set; }
        public required Guid MarketId { get; set; }
        public Market Market { get; set; }
        public bool Success { get; set; } = true;
    }

    public class PurchaseEntityConfig : IEntityTypeConfiguration<Purchase>
    {
        public void Configure(EntityTypeBuilder<Purchase> builder)
        {
            builder.HasOne(x=>x.Customer).WithMany(x=>x.Purchases).HasForeignKey(x=>x.CustomerId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
