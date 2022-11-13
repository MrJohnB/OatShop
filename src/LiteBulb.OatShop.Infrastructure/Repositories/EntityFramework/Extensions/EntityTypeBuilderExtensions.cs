using LiteBulb.OatShop.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace LiteBulb.OatShop.Infrastructure.Repositories.EntityFramework.Extensions;
public static class EntityTypeBuilderExtensions
{
    public static void ConfigureEntities(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>().ToTable("Customer");
        modelBuilder.Entity<Product>().ToTable("Product");
        modelBuilder.Entity<Order>().ToTable("Order");
        modelBuilder.Entity<OrderItem>().ToTable("OrderItem");
    }
}
