using LiteBulb.OatShop.ApplicationCore.Configuration;
using LiteBulb.OatShop.Infrastructure.Repositories.EntityFramework.Configuration;

namespace LiteBulb.OatShop.Api;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        // Add MVC services
        builder.Services.AddControllers(options =>
        {
            // ASP.NET Core trims the suffix Async from action names by default
            options.SuppressAsyncSuffixInActionNames = false; // re-enable
        });

        // Add EntityFramework Core
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        builder.Services.AddApplicationDbContext(connectionString);

        // Add service registrations
        builder.Services.AddRepositories();
        builder.Services.AddServices();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
