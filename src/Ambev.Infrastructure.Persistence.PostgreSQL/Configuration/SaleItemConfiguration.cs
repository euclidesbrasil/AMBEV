﻿using Ambev.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.Infrastructure.Persistence.PostgreSQL.Configuration
{
    public class SaleItemConfiguration : IEntityTypeConfiguration<SaleItem>
    {
        public void Configure(EntityTypeBuilder<SaleItem> builder)
        {
            builder.ToTable("SaleItems");

            builder.HasKey(i => i.Id);

            builder.Property(i => i.SaleId)
                .IsRequired();

            builder.Property(i => i.ProductId)
                .IsRequired();

            builder.Property(i => i.ProductName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(i => i.Quantity)
                .IsRequired();

            builder.Property(i => i.UnitPrice)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(i => i.Discount)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(i => i.TotalPrice)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(i => i.IsCancelled)
                .IsRequired();
        }
    }
}
