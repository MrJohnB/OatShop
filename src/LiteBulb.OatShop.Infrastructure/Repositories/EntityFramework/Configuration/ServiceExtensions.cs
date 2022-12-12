using LiteBulb.OatShop.Domain.Dtos;
using LiteBulb.OatShop.Infrastructure.Mappers;
using LiteBulb.OatShop.Shared.Mappers;
using LiteBulb.OatShop.Shared.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace LiteBulb.OatShop.Infrastructure.Repositories.EntityFramework.Configuration;
public static class ServiceExtensions
{
    public static IServiceCollection AddApplicationDbContext(this IServiceCollection services, string connectionString)
    {
        if (string.IsNullOrWhiteSpace(connectionString)) { throw new ArgumentNullException(nameof(connectionString)); }

        var serverVersion = ServerVersion.AutoDetect(connectionString);

        // For common usages, see pull request #1233.
        // Default service lifetime is Scoped
        return services.AddDbContext<OatShopDbContext>(
            optionsBuilder => optionsBuilder
                .UseMySql(connectionString, serverVersion, options => options
                    .EnableRetryOnFailure(
                        maxRetryCount: 3,
                        maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorNumbersToAdd: null))
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                // The following three options help with debugging, but should
                // be changed or removed for production.
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors()
            );
    }

    public static IServiceCollection AddMappers(this IServiceCollection services)
    {
        return services
            .AddSingleton<IMapper<Entities.Customer, Customer>, CustomerMapper>()
            .AddSingleton<IMapper<Entities.OrderItem, OrderItem>, OrderItemMapper>()
            .AddSingleton<IMapper<Entities.Order, Order>, OrderMapper>()
            .AddSingleton<IMapper<Entities.Product, Product>, ProductMapper>();
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        return services
            .AddScoped<IRepository<Customer, int>, CustomerRepository>()
            .AddScoped<IRepository<OrderItem, int>, OrderItemRepository>()
            .AddScoped<IRepository<Order, int>, OrderRepository>()
            .AddScoped<IRepository<Product, int>, ProductRepository>();
            //.AddScoped<IRepository<Product, int>>(serviceProvider =>
            //    new CachedRepository<Product, int>(
            //        logger: serviceProvider.GetRequiredService<ILogger<ProductRepository>>(),
            //        repository: new ProductRepository(
            //            serviceProvider.GetRequiredService<ILogger<ProductRepository>>(),
            //            serviceProvider.GetRequiredService<OatShopDbContext>(),
            //            serviceProvider.GetRequiredService<IMapper<Entities.Product, Product>>())));
    }
}
