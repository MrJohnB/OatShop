using LiteBulb.OatShop.Domain.Extensions;

namespace LiteBulb.OatShop.Domain.Dtos;
public class OrderItem : Dto
{
    public int OrderId { get; set; }
    public Product? Product { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal OriginalPrice { get; set; }
    public decimal Discount { get; set; }

    // Calculated on the fly:
    public decimal NetPrice => this.CalculateItemNetPrice();
}
