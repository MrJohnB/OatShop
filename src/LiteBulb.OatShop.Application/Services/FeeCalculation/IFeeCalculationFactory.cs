namespace LiteBulb.OatShop.Application.Services.FeeCalculation;
public interface IFeeCalculationFactory
{
    IFeeMethod GetFeeMethod(FeeMethodType feeMethodType);
}
