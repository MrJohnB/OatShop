using LiteBulb.OatShop.Domain.Dtos;
using LiteBulb.OatShop.Infrastructure.Shared.Repositories.EntityFramework;
using LiteBulb.OatShop.Shared.Mappers;
using Microsoft.Extensions.Logging;

namespace LiteBulb.OatShop.Infrastructure.Repositories.EntityFramework;
public class ProductRepository : AuditableRepository<Entities.Product, Product, int>
{
    public ProductRepository(ILogger<ProductRepository> logger, OatShopDbContext dbContext, IMapper<Entities.Product, Product> mapper)
        : base(logger, dbContext, mapper) { }
}
