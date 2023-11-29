using CCKLDemo.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace CCKLDemo.Database
{
    public class AccountDbContext:DbContext
    {
        public AccountDbContext(DbContextOptions<AccountDbContext> options):base(options)
        {
            var dbCreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
            if (dbCreator != null)
            {
                if (!dbCreator.CanConnect())
                    dbCreator.Create();
                if (!dbCreator.HasTables())
                    dbCreator.CreateTables();
            }
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Market> Markets { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Credit> Credits { get; set; }
        public DbSet<Purchase> Purchases { get; set; }

    }
}
