using System.Collections.ObjectModel;
using LiteBulb.OatShop.ApplicationCore.Enumerations;
using LiteBulb.OatShop.SharedKernel.Entities;

namespace LiteBulb.OatShop.Infrastructure.Entities;
public class Order : Entity, IEntity<int>
{
    public int Id { get; set; }
    public int CompanyId { get; set; }
    public int CustomerId { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public decimal Discount { get; set; }
    public virtual ICollection<OrderItem> OrderItems { get; set; } = new Collection<OrderItem>();
}
