using LiteBulb.OatShop.Infrastructure.Repositories.EntityFramework.Extensions;
using Microsoft.EntityFrameworkCore;

namespace LiteBulb.OatShop.Infrastructure.Repositories.EntityFramework;
public class OatShopDbContext : DbContext
{
    //public DbSet<Customer> Customers => Set<Customer>();
    //public DbSet<Order> Orders => Set<Order>();
    //public DbSet<OrderItem> Customers => Set<OrderItem>();
    //public DbSet<Product> Orders => Set<Product>();

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
