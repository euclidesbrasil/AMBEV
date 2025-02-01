using Ambev.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.Infrastructure.Persistence.PostgreSQL.Configuration
{
    public class SaleConfiguration : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            builder.ToTable("Sales"); // Define o nome da tabela

            builder.HasKey(s => s.Id); // Define a chave primária

            builder.Property(s => s.SaleNumber)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(s => s.SaleDate)
                .IsRequired();

            builder.Property(s => s.UserId)
                .IsRequired();

            builder.Property(s => s.UserFirstName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(s => s.BranchId)
                .IsRequired();

            builder.Property(s => s.BranchName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(s => s.TotalAmount)
                .HasPrecision(18, 2);

            builder.Property(s => s.IsCancelled)
                .IsRequired();

            builder.HasMany(s => s.Items)
                .WithOne()
                .HasForeignKey(i => i.SaleId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
