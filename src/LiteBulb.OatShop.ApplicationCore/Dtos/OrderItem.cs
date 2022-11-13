using LiteBulb.OatShop.ApplicationCore.Extensions;

namespace LiteBulb.OatShop.ApplicationCore.Dtos;
public class OrderItem
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public Product? Product { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal OriginalPrice { get; set; }
    public decimal Discount { get; set; }
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }

    // Calculated on the fly:
    public decimal NetPrice => this.CalculateItemNetPrice();
}
