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
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customers"); // Define o nome da tabela

            builder.HasKey(s => s.Id); // Define a chave primária

            builder.Property(s => s.FirstName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(s => s.LastName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(s => s.Identification)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
