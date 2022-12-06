namespace LiteBulb.OatShop.Shared.Entities;
public abstract class Auditable : IAuditable
{
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
}
