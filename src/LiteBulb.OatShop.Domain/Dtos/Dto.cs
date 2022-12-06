using LiteBulb.OatShop.Shared.Entities;

namespace LiteBulb.OatShop.Domain.Dtos;
public abstract class Dto : Auditable, IEntity<int>
{
    public int Id { get; set; }
}
