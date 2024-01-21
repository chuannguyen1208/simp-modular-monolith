using Serilog;
using Simp.Modules.Blogs.Api;
using Simp.Modules.Cshops.Api;
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
    
    builder.Host.UseSerilog();

    builder.AddBlogsModule();
    builder.AddCshopsModule();
    builder.AddInfrastructure();

    var app = builder.Build();

    app.UseInfrastructure();

    app.MapGet("/", () => "Modular monolith api");

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