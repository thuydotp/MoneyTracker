﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MoneyTracker.Persistence.Mapping
{
    public class SpendingItemMapping : IEntityTypeConfiguration<SpendingItemDA>
    {
        public void Configure(EntityTypeBuilder<SpendingItemDA> builder)
        {
            builder.ToTable("Transactions");
            
            builder.HasKey(x => x.ID);

            builder.Property(x => x.ID)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Type)
                .HasDefaultValue(SpendingTypeDA.Expense)
                .IsRequired();

            builder.Property(x => x.ChangeValue)
                .IsRequired();

            builder.Property(x => x.Description)
                .HasMaxLength(250)
                .IsRequired();

            builder.Property(x => x.RecordDate)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("getdate()")
                .IsRequired();
        }
    }
}
