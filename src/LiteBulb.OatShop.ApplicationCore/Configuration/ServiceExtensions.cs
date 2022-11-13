using LiteBulb.OatShop.ApplicationCore.Interfaces.Services;
using LiteBulb.OatShop.ApplicationCore.Interfaces.Services.FeeCalculation;
using LiteBulb.OatShop.ApplicationCore.Services;
using LiteBulb.OatShop.ApplicationCore.Services.FeeCalculation;
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
            // AddSingleton<IProductService, CachedProductService>();
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

    public static IServiceCollection AddClassWithoutInterface(this IServiceCollection services)
    {
        return services
            .AddTransient<CustomerService>(); // Note: no interface!
    }
}
