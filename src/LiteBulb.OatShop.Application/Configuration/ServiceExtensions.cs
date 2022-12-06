using LiteBulb.OatShop.Application.Dtos;
using LiteBulb.OatShop.Application.Services.Data;
using LiteBulb.OatShop.Application.Services.FeeCalculation;
using LiteBulb.OatShop.Shared.Services.Data;
using Microsoft.Extensions.DependencyInjection;

namespace LiteBulb.OatShop.Application.Configuration;
public static class ServiceExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        return services
            .AddScoped<IService<Customer>, CustomerService>()
            .AddScoped<IService<Order>, OrderService>()
            .AddScoped<IService<Product>, ProductService>();
    }

    /// <summary>
    /// Look at how these utilize Service Locator pattern (which is an anti-pattern).
    /// </summary>
    /// <param name="services">IServiceCollection instance</param>
    /// <returns>IServiceCollection instance</returns>
    public static IServiceCollection AddFeeCalculationMethods(this IServiceCollection services)
    {
        return services
            .AddSingleton<IFeeCalculationFactory, FeeCalculationFactory>()
            .AddSingleton<IFeeMethod, FeeMethodA>()
            .AddSingleton<IFeeMethod, FeeMethodB>();

            // Note: When you call: serviceProvider.GetServices<IFeeMethod>()
            // there's more than one under than interface, so you get a list of
            // objects in the same order that they were registered/mapped in.
    }
}
