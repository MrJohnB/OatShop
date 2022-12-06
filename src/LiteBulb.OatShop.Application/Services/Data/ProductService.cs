using LiteBulb.OatShop.Application.Dtos;
using LiteBulb.OatShop.SharedKernel.Repositories;
using LiteBulb.OatShop.SharedKernel.Services.Data;
using Microsoft.Extensions.Logging;

namespace LiteBulb.OatShop.Application.Services.Data;
public class ProductService : Service<Product>
{
    public ProductService(ILogger<ProductService> logger, IRepository<Product> productRepository)
        : base(logger, productRepository) { }
}
