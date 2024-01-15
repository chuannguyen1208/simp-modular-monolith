using Serilog;
using Simp.Shared.Infrastructure;

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .CreateLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddInfrastructure();

    builder.Host.UseSerilog();

    var app = builder.Build();

    app.UseInfrastructure();

    app.MapGet("/", () =>
    {
        return Results.Ok("Modular monolith api");
    });

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "An unhandled exception occurred during bootstrapping");
}
finally
{
    Log.CloseAndFlush();
}


public partial class Program { }