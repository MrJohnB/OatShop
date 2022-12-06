using LiteBulb.OatShop.SharedKernel.Entities;
namespace LiteBulb.OatShop.Application.Dtos;
public abstract class Dto : Auditable, IEntity<int>
{
    public int Id { get; set; }
}
