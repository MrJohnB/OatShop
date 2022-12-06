using LiteBulb.OatShop.Shared.Entities;

namespace LiteBulb.OatShop.Infrastructure.Entities;
public abstract class Entity : Auditable, IEntity<int>
{
    public int Id { get; set; }
}
