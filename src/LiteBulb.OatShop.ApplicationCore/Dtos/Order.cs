using LiteBulb.OatShop.ApplicationCore.Enumerations;
using LiteBulb.OatShop.ApplicationCore.Extensions;

namespace LiteBulb.OatShop.ApplicationCore.Dtos;
public class Order : Dto
{
    public int CompanyId { get; set; }
    public int CustomerId { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public decimal Discount { get; set; }
    public virtual IReadOnlyCollection<OrderItem> OrderItems { get; set; } = null!;

    // Calculated on the fly:
    public decimal Subtotal => this.CalculateOrderSubtotal();
    public decimal Total => this.CalculateOrderTotal();
}
