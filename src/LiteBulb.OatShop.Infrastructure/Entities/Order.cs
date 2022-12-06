using LiteBulb.OatShop.Application.Enumerations;

namespace LiteBulb.OatShop.Infrastructure.Entities;
public class Order : Entity
{
    public int CompanyId { get; set; }
    public int CustomerId { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public decimal Discount { get; set; }
    public virtual IReadOnlyCollection<OrderItem> OrderItems { get; set; } = null!;
}
