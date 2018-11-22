using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MoneyTracker.Persistence.Mapping
{
    public class SpendingAccountMapping : IEntityTypeConfiguration<SpendingAccountDA>
    {
        public void Configure(EntityTypeBuilder<SpendingAccountDA> builder)
        {
            builder.ToTable("SpendingAccounts");
            builder.HasKey(x => x.ID);

            builder.Property(x => x.ID)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.AccountName)
                .HasMaxLength(200)
                .IsRequired();

            builder.HasMany(x => x.Transactions)
                .WithOne(x => x.SpendingAccount)
                .HasForeignKey(x => x.SpendingAccountID);
        }
    }
}
