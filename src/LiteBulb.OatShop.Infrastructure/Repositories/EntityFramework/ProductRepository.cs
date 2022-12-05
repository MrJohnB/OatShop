using LiteBulb.OatShop.ApplicationCore.Dtos;
using LiteBulb.OatShop.SharedKernel.Mappers;
using LiteBulb.OatShop.SharedKernel.Repositories.EntityFramework;
using Microsoft.Extensions.Logging;

namespace LiteBulb.OatShop.Infrastructure.Repositories.EntityFramework;
public class ProductRepository : Repository<Entities.Product, Product>
{
    public ProductRepository(ILogger<ProductRepository> logger, OatShopDbContext dbContext, IMapper<Entities.Product, Product> mapper)
        : base(logger, dbContext, mapper) { }
}
