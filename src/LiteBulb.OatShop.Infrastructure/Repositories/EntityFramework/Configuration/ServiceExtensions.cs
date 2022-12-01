using LiteBulb.OatShop.ApplicationCore.Dtos;
using LiteBulb.OatShop.ApplicationCore.Interfaces.Repositories.Data;
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
        return services.AddDbContext<OatShopDbContext>(
            optionsBuilder => optionsBuilder
                .UseMySql(connectionString, serverVersion)
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                // The following three options help with debugging, but should
                // be changed or removed for production.
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors()
            );
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        return services
            .AddScoped<IRepository<Customer>, CustomerRepository>()
            .AddScoped<IRepository<OrderItem>, OrderItemRepository>()
            .AddScoped<IRepository<Order>, OrderRepository>()
            .AddScoped<IRepository<Product>, ProductRepository>();
            //.AddScoped<IRepository<Product>, CachedProductRepository>();
    }
}
