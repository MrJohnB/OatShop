using LiteBulb.OatShop.Domain.Dtos;
using LiteBulb.OatShop.Shared.Repositories;
using LiteBulb.OatShop.Shared.Services.Data;
using Microsoft.Extensions.Logging;

namespace LiteBulb.OatShop.Application.Services.Data;
public class ProductService : Service<Product, int>
{
    public ProductService(ILogger<ProductService> logger, IRepository<Product, int> productRepository)
        : base(logger, productRepository) { }
}
