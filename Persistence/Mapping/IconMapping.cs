using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MoneyTracker.Persistence.Mapping
{
    public class IconMapping : IEntityTypeConfiguration<IconDA>
    {
        public void Configure(EntityTypeBuilder<IconDA> builder)
        {
            builder.ToTable("Icons");
            
            builder.HasKey(x => x.ID);

            builder.Property(x => x.ID)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.IconName)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(x => x.IconKey)
                .HasMaxLength(200)
                .IsRequired();
        }
    }
}
