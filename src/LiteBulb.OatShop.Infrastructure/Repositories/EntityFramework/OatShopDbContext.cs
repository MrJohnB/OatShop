using LiteBulb.OatShop.Infrastructure.Repositories.EntityFramework.Extensions;
using Microsoft.EntityFrameworkCore;

namespace LiteBulb.OatShop.Infrastructure.Repositories.EntityFramework;
public class OatShopDbContext : DbContext
{
    public OatShopDbContext(DbContextOptions<OatShopDbContext> options)
        : base(options) { }

    protected OatShopDbContext(DbContextOptions options)
    : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ConfigureEntities();
    }
}
