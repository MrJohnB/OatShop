using LiteBulb.OatShop.Shared.Entities;

namespace LiteBulb.OatShop.Domain.Dtos;
public abstract class Dto : IEntity<int>
{
    public int Id { get; set; }
    public DateTimeOffset? Created { get; set; }
    public DateTimeOffset? LastModified { get; set; }
}
