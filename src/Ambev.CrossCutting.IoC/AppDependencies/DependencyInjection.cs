using Ambev.Core.Domain.Interfaces;
using Ambev.Infrastructure.Persistence.PostgreSQL.Context;
using Ambev.Infrastructure.Persistence.PostgreSQL.Repositories;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Reflection;
using Ambev.Infrastructure.Persistence.MongoDB.Configuration;
using Ambev.Infrastructure.Persistence.Repositories;
using Ambev.Infrastructure.Persistence.MongoDB.Service;
using Ambev.Core.Application.UseCases.Mapper;
using Ambev.Core.Application.Shared.Behavior;
using Ambev.Infrastructure.Security;
namespace Ambev.Infrastructure.CrossCutting.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Postgres
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            // MongoDB
            services.Configure<MongoDbSettings>(configuration.GetSection("MongoDbSettings"));

            // Registra o cliente do MongoDB
            services.AddSingleton<IMongoClient>(serviceProvider =>
            {
                var settings = serviceProvider.GetRequiredService<IOptions<MongoDbSettings>>().Value;
                return new MongoClient(settings.ConnectionString);
            });

            // Registra o banco de dados do MongoDB
            services.AddScoped<IMongoDatabase>(serviceProvider =>
            {
                var client = serviceProvider.GetRequiredService<IMongoClient>();
                var settings = serviceProvider.GetRequiredService<IOptions<MongoDbSettings>>().Value;
                return client.GetDatabase(settings.DatabaseName);
            });


            services.AddScoped<ISaleRepository, SaleRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<JwtTokenService>();
            services.AddScoped<IJwtTokenService, JwtTokenService>();
            services.AddScoped<CounterService>();
            services.AddAutoMapper(typeof(CommonMapper));
            
            var myhandlers = AppDomain.CurrentDomain.Load("Ambev.Core.Application");
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(myhandlers);
                cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
            });

            services.AddValidatorsFromAssembly(Assembly.Load("Ambev.Core.Application"));

            return services;


        }
    }
}
