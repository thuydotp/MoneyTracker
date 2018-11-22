using Microsoft.EntityFrameworkCore;
using MoneyTracker.Persistence.Mapping;

namespace MoneyTracker.Persistence
{
    public class MoneyTrackerContext : DbContext
    {
        public MoneyTrackerContext(DbContextOptions<MoneyTrackerContext> options)
            : base(options)
        {
        }

        public DbSet<IconDA> Icons { get; set; }
        public DbSet<CategoryDA> Categories { get; set; }
        public DbSet<SpendingAccountDA> Accounts { get; set; }
        public DbSet<TransactionDA> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new SpendingAccountMapping());
            modelBuilder.ApplyConfiguration(new CategoryMapping());
            modelBuilder.ApplyConfiguration(new IconMapping());
            modelBuilder.ApplyConfiguration(new TransactionMapping());
        }

    }
}
