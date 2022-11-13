using System.Collections.ObjectModel;
using LiteBulb.OatShop.ApplicationCore.Enumerations;

namespace LiteBulb.OatShop.Infrastructure.Entities;
public class Order
{
    public int Id { get; set; }
    public int CompanyId { get; set; }
    public int CustomerId { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public decimal Discount { get; set; }
    public virtual ICollection<OrderItem> OrderItems { get; set; } = new Collection<OrderItem>();
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
}
