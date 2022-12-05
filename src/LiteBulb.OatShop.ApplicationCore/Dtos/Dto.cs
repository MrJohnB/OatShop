using LiteBulb.OatShop.SharedKernel.Entities;
namespace LiteBulb.OatShop.ApplicationCore.Dtos;
public abstract class Dto : Auditable, IEntity<int>
{
    public int Id { get; set; }
}
