using LiteBulb.OatShop.ApplicationCore.Configuration;
using LiteBulb.OatShop.Infrastructure.Repositories.EntityFramework.Configuration;
using Serilog;

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

        // Add custom service registrations
        builder.Services.AddRepositories();
        builder.Services.AddServices();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // Logging
        builder.Host.UseSerilog((context, configuration) =>
            configuration.ReadFrom.Configuration(context.Configuration));

        //builder.Services.BuildServiceProvider(validateScopes: true);

        var app = builder.Build();

        app.UseSerilogRequestLogging();

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
