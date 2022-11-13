namespace LiteBulb.OatShop.ApplicationCore.Dtos;
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public decimal OriginalPrice { get; set; }
    public decimal Discount { get; set; }
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
}
