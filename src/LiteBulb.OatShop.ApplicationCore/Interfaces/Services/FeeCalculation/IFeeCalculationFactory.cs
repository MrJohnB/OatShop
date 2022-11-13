namespace LiteBulb.OatShop.ApplicationCore.Interfaces.Services.FeeCalculation;
public interface IFeeCalculationFactory
{
    IFeeMethod GetFeeMethod(FeeMethodType feeMethodType);
}