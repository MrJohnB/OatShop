namespace LiteBulb.OatShop.SharedKernel.Entities;
public interface IEntity<TId>
{
    TId Id { get; protected set; }
}
