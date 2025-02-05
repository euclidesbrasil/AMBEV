using Entities = Ambev.Core.Domain.Entities;
using Ambev.Infrastructure.Persistence.PostgreSQL.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ambev.Core.Domain.Interfaces;

namespace Ambev.Infrastructure.Persistence.PostgreSQL.Seed
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider, IJwtTokenService tokenService)
        {
            using (var context = new AppDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>()))
            {
                if (context.Users.Any())
                {
                    return;
                }

                IJwtTokenService _tokenService = tokenService;
                context.Users.AddRange(
                    new Entities.User("user.admin.ambev@ambev.com.br", "admin", "@mbev", "User", "Admin",
                    // public Address(string city, string street, int number, string zipCode, Geolocation geolocation)
                    new Core.Domain.ValueObjects.Address("Fortaleza", "Rua em Fortaleza", 1000, "60000-000", new Core.Domain.ValueObjects.Geolocation("-3.71839", "-38.5434")),
                    "85999999999", Core.Domain.Enum.UserStatus.Active, Core.Domain.Enum.UserRole.Admin, _tokenService)

                );

                context.Branches.AddRange(
                        new Entities.Branch { Name = "Matriz", Location = "Fortaleza" },
                        new Entities.Branch { Name = "Filial 1", Location = "Fortaleza" },
                        new Entities.Branch { Name = "Filial 2", Location = "Fortaleza" },
                        new Entities.Branch { Name = "Filial 3", Location = "Fortaleza" }
                );

                context.Customers.AddRange(
                        new Entities.Customer() { FirstName = "Cliente 1", LastName = "Sobrenome 1", Identification = "112345678901" },
                        new Entities.Customer() { FirstName = "Cliente 2", LastName = "Sobrenome 2", Identification = "222345678901" },
                        new Entities.Customer() { FirstName = "Cliente 3", LastName = "Sobrenome 3", Identification = "322345678901" },
                        new Entities.Customer() { FirstName = "Cliente 4", LastName = "Sobrenome 4", Identification = "422345678901" },
                        new Entities.Customer() { FirstName = "Cliente 5", LastName = "Sobrenome 5", Identification = "522345678901" });

                context.Products.AddRange(
                        new Entities.Product("Guaraná", 5.00m, "Refrigerante da fruta", "Refrigerante", "http://fake.img.com", new Core.Domain.ValueObjects.Rating(5, 1000)),
                        new Entities.Product("Pepsi ", 4.00m, "Refrigerante cola", "Refrigerante", "http://fake.img.com", new Core.Domain.ValueObjects.Rating(5, 100)),
                        new Entities.Product("Antarctica", 8.00m, "Cerveja mista", "Cerveja", "http://fake.img.com", new Core.Domain.ValueObjects.Rating(5, 150)),
                        new Entities.Product("Patagonia ", 8.00m, "Cerveja puro malte", "Cerveja", "http://fake.img.com", new Core.Domain.ValueObjects.Rating(5, 850)),
                        new Entities.Product("Skol  ", 4.50m, "Cerveja mista", "Cerveja", "http://fake.img.com", new Core.Domain.ValueObjects.Rating(5, 150))
                        );

                context.SaveChanges();
            }
        }
    }
}
