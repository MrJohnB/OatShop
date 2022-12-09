namespace LiteBulb.OatShop.Domain.Dtos;
public abstract class Dto
{
    public int Id { get; set; }
    public DateTimeOffset? Created { get; set; }
    public DateTimeOffset? LastModified { get; set; }
}
