
namespace LiteBulb.OatShop.Shared.Entities;
public interface IAuditable
{
    DateTime Created { get; protected set; }
    DateTime Updated { get; protected set; }
}
