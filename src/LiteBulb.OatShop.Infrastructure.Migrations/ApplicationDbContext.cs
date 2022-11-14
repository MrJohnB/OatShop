using LiteBulb.OatShop.Infrastructure.Repositories.EntityFramework.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace LiteBulb.OatShop.Infrastructure.Migrations
{
    public class ApplicationDbContext : DbContext
    {
        private const string ConnectionStringName = "DefaultConnection";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder); // The base implementation does nothing.

            var connectionString = GetConnectionString();

            // Alternatively, for specific version use:
            // new MariaDbServerVersion(new Version(10, 6, 5, 0));
            // new MySqlServerVersion(new Version(8, 0));
            var serverVersion = ServerVersion.AutoDetect(connectionString);

            // For common usages, see pull request #1233.
            optionsBuilder.UseMySql(connectionString, serverVersion);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ConfigureEntities();
        }

        private static string? GetConnectionString()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddUserSecrets<ApplicationDbContext>(optional: true)
                .Build();

            return configuration.GetConnectionString(ConnectionStringName);
        }
    }
}
