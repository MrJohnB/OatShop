using LiteBulb.OatShop.ApplicationCore.Dtos;

namespace LiteBulb.OatShop.ApplicationCore.Extensions;
public static class OrderExtensions
{
    public static decimal CalculateOrderSubtotal(this Order order)
    {
        return order.OrderItems.Sum(x => x.NetPrice);
    }

    public static decimal CalculateOrderTotal(this Order order)
    {
        var discount = Math.Round(
                order.Subtotal * order.Discount,
                MidpointRounding.ToEven);

        return order.Subtotal - discount;
    }

    public static decimal CalculateItemNetPrice(this OrderItem orderItem)
    {
        var discount = Math.Round(
                orderItem.OriginalPrice * orderItem.Discount,
                MidpointRounding.ToEven);

        return orderItem.OriginalPrice - discount;
    }
}
