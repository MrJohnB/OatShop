namespace LiteBulb.OatShop.SharedKernel.Entities;
public interface IEntity<TId>
{
    TId Id { get; set; }
    DateTime Created { get; set; }
    DateTime Updated { get; set; }
}
