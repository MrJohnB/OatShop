using LiteBulb.OatShop.Domain.Dtos;

namespace LiteBulb.OatShop.Domain.Extensions
{
    internal static class OrderExtensions
    {
        internal static decimal CalculateOrderSubtotal(this Order order)
        {
            return order.OrderItems.Sum(x => x.NetPrice);
        }

        internal static decimal CalculateOrderTotal(this Order order)
        {
            var discount = Math.Round(
            order.Subtotal * order.Discount,
            MidpointRounding.ToEven);

            return order.Subtotal - discount;
        }

        internal static decimal CalculateItemNetPrice(this OrderItem orderItem)
        {
            var discount = Math.Round(
            orderItem.OriginalPrice * orderItem.Discount,
            MidpointRounding.ToEven);

            return orderItem.OriginalPrice - discount;
        }
    }
}
