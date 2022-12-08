namespace LiteBulb.OatShop.Shared.Entities;
public interface IAuditable
{
    DateTimeOffset Created { get; set; }
    DateTimeOffset LastModified { get; set; }
}
