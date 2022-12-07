
namespace LiteBulb.OatShop.Shared.Entities;
public interface IAuditable
{
    DateTimeOffset Created { get; protected set; }
    DateTimeOffset LastModified { get; protected set; }
}
