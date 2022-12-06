namespace LiteBulb.OatShop.Domain.Dtos
{
    public class Product : Dto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public decimal OriginalPrice { get; set; }
        public decimal Discount { get; set; }
    }
}
