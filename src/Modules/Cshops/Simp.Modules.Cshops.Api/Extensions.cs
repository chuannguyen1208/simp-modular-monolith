using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Simp.Modules.Cshops.Infrastructure;
using Simp.Modules.Cshops.Infrastructure.AutofacModules;
using Simp.Modules.Cshops.Infrastructure.EF;

namespace Simp.Modules.Cshops.Api;
public static class Extensions
{
    public static void AddCshopsModule(this WebApplicationBuilder builder)
    {
        builder.Host.ConfigureContainer<ContainerBuilder>((_, cb) =>
        {
            var connectionString = builder.Configuration.GetConnectionString("cshop") ?? "";

            cb.RegisterModule(new CshopDbContextModule(connectionString));
            cb.RegisterType<CshopsCompositionRoot>().As<ICshopsCompositionRoot>().SingleInstance();
        });
    }

    public static void UseCshopsModule(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<CshopDbContext>();

        var connectionString = dbContext.Database.GetConnectionString();

        Log.Information($"Cshop connection string: {connectionString}");

        if (dbContext.Database.IsRelational())
        {
            dbContext.Database.Migrate();
        }
    }
}
