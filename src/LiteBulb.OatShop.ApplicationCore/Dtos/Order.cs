using System.Collections.ObjectModel;
using LiteBulb.OatShop.ApplicationCore.Enumerations;
using LiteBulb.OatShop.ApplicationCore.Extensions;
using LiteBulb.OatShop.SharedKernel.Entities;

namespace LiteBulb.OatShop.ApplicationCore.Dtos;
public class Order : Entity, IEntity<int>
{
    public int Id { get; set; }
    public int CompanyId { get; set; }
    public int CustomerId { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public decimal Discount { get; set; }
    public virtual ICollection<OrderItem> OrderItems { get; set; } = new Collection<OrderItem>();

    // Calculated on the fly:
    public decimal Subtotal => this.CalculateOrderSubtotal();
    public decimal Total => this.CalculateOrderTotal();
}
