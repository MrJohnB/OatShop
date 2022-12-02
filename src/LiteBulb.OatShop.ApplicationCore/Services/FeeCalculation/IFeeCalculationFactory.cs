namespace LiteBulb.OatShop.ApplicationCore.Services.FeeCalculation;
public interface IFeeCalculationFactory
{
    IFeeMethod GetFeeMethod(FeeMethodType feeMethodType);
}
