using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MoneyTracker.Persistence.Mapping
{
    public class CategoryMapping : IEntityTypeConfiguration<CategoryDA>
    {
        public void Configure(EntityTypeBuilder<CategoryDA> builder)
        {
            builder.ToTable("Categories");
            builder.HasKey(x => x.ID);

            builder.Property(x => x.ID)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.CategoryName)
                .HasMaxLength(200)
                .IsRequired();

            builder.HasMany(x => x.Transactions)
                .WithOne(x => x.Category)
                .HasForeignKey(x => x.CategoryID);
                
            builder.Property(x => x.Type)
                .HasDefaultValue(SpendingTypeDA.Expense)
                .IsRequired();
        }
    }
}
