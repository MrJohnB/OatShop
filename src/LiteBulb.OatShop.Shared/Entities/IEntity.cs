namespace LiteBulb.OatShop.Shared.Entities;
public interface IEntity<TId>
{
    TId Id { get; set; }
}
