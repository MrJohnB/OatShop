using LiteBulb.OatShop.ApplicationCore.Interfaces.Services.FeeCalculation;
using Microsoft.Extensions.DependencyInjection;

namespace LiteBulb.OatShop.ApplicationCore.Services.FeeCalculation;

/// <summary>
/// Service Locator Pattern
/// 
/// Bad anti-pattern because:
/// 1. This factory might be aware of concrete implementations of the FeeMethod strategies (bad).
/// 2. TODO: list more.
/// </summary>
public class FeeCalculationFactory : IFeeCalculationFactory
{
    private readonly IServiceProvider _serviceProvider;

    public FeeCalculationFactory(IServiceProvider serviceProvider)
    {
        ArgumentNullException.ThrowIfNull(nameof(serviceProvider));

        _serviceProvider = serviceProvider;
    }

    public IFeeMethod GetFeeMethod(FeeMethodType feeMethodType)
    {
        var services = _serviceProvider.GetServices<IFeeMethod>();

        switch (feeMethodType)
        {
            case FeeMethodType.A:
                return services.FirstOrDefault();
            case FeeMethodType.B:
                return services.LastOrDefault();
            case FeeMethodType.None:
            default:
                throw new NotSupportedException();
        }
    }
}
