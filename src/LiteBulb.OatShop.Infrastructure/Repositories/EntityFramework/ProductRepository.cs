using LiteBulb.OatShop.Application.Dtos;
using LiteBulb.OatShop.Shared.Mappers;
using LiteBulb.OatShop.Shared.Repositories.EntityFramework;
using Microsoft.Extensions.Logging;

namespace LiteBulb.OatShop.Infrastructure.Repositories.EntityFramework;
public class ProductRepository : Repository<Entities.Product, Product>
{
    public ProductRepository(ILogger<ProductRepository> logger, OatShopDbContext dbContext, IMapper<Entities.Product, Product> mapper)
        : base(logger, dbContext, mapper) { }
}
