using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Ambev.Core.Domain.Entities;

namespace CleanArch.Infrastructure.PostgreSQL.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products"); // Define o nome da tabela

            builder.HasKey(p => p.Id); // Define a chave primária

            builder.Property(p => p.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(p => p.Price)
                .HasPrecision(18, 2); // Define precisão para valores decimais

            builder.Property(p => p.Description)
                .HasMaxLength(1000);

            builder.Property(p => p.Category)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.Image)
                .HasMaxLength(500);

            // Configuração do Value Object como Owned Type
            builder.OwnsOne(p => p.Rating, rating =>
            {
                rating.Property(r => r.Rate)
                    .HasColumnType("decimal(2,1)")
                    .IsRequired();

                rating.Property(r => r.Count)
                    .IsRequired();
            });
        }
    }
}
