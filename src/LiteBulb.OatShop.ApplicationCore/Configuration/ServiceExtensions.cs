using LiteBulb.OatShop.ApplicationCore.Interfaces.Services;
using LiteBulb.OatShop.ApplicationCore.Services;
using Microsoft.Extensions.DependencyInjection;

namespace LiteBulb.OatShop.ApplicationCore.Configuration;
public static class ServiceExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        return services
            .AddScoped<ICustomerService, CustomerService>()
            .AddScoped<IOrderService, OrderService>()
            .AddScoped<IProductService, ProductService>();
    }
}
