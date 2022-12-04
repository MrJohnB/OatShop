using LiteBulb.OatShop.ApplicationCore.Configuration;
using LiteBulb.OatShop.Infrastructure.Repositories.EntityFramework.Configuration;
using Microsoft.OpenApi.Models;
using Serilog;

namespace LiteBulb.OatShop.Api;

public static class Program
{
    private const string ConnectionStringName = "DefaultConnection";

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
        var connectionString = builder.Configuration.GetConnectionString(ConnectionStringName);
        builder.Services.AddApplicationDbContext(connectionString);

        // Add custom service registrations
        builder.Services.AddMappers();
        builder.Services.AddRepositories();
        builder.Services.AddServices();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1",
                new OpenApiInfo
                {
                    Title = "OatShop API",
                    Version = "v1",
                    Description = "RESTful endpoints to be consumed by OatShop"
                });

            var filePath = Path.Combine(AppContext.BaseDirectory, "LiteBulb.OatShop.Api.xml");
            options.IncludeXmlComments(filePath);
        });

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
        app.UseCors(options => options // Call to UseCors() must be placed after UseRouting, but before UseAuthorization
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod());

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
