using LiteBulb.OatShop.Infrastructure.Repositories.EntityFramework.Extensions;
using Microsoft.EntityFrameworkCore;

namespace LiteBulb.OatShop.Infrastructure.Migrations
{
    public class ApplicationDbContext : DbContext
    {
        private const string ConnectionString = "#{DatabaseConnectionString}";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder); // The base implementation does nothing.

            // Alternatively, for specific version use:
            // new MariaDbServerVersion(new Version(10, 6, 5, 0));
            // new MySqlServerVersion(new Version(8, 0));
            var serverVersion = ServerVersion.AutoDetect(ConnectionString);

            // For common usages, see pull request #1233.
            optionsBuilder.UseMySql(ConnectionString, serverVersion);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ConfigureEntities();
        }
    }
}
