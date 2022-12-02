using LiteBulb.OatShop.SharedKernel.Entities;

namespace LiteBulb.OatShop.ApplicationCore.Dtos;
public class Product : Entity, IEntity<int>
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public decimal OriginalPrice { get; set; }
    public decimal Discount { get; set; }
}
