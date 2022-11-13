namespace LiteBulb.OatShop.Infrastructure.Entities;
public class OrderItem
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    //public int ProductId { get; set; }
    public virtual Product? Product { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal OriginalPrice { get; set; }
    public decimal Discount { get; set; }
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
}
