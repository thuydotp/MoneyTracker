using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MoneyTracker.Persistence.Mapping
{
    public class CategoryMapping : IEntityTypeConfiguration<CategoryDA>
    {
        public void Configure(EntityTypeBuilder<CategoryDA> builder)
        {
            builder.HasKey(x => x.ID);

            builder.Property(x => x.ID)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.CategoryName)
                .HasMaxLength(200)
                .IsRequired();
        }
    }
}
