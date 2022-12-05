namespace LiteBulb.OatShop.Infrastructure.Entities;
public class OrderItem : Entity
{
    public int OrderId { get; set; }
    //public int ProductId { get; set; }
    public virtual Product? Product { get; set; } // TODO: should OrderItem.ProductId (FK) be nullable?
    public string Name { get; set; } = string.Empty;
    public decimal OriginalPrice { get; set; }
    public decimal Discount { get; set; }
}
