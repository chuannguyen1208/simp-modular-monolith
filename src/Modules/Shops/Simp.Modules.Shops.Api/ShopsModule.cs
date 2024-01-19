using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Simp.Modules.Shops.Infrastructure.EF;

namespace Simp.Modules.Shops.Api;

internal static class ShopsModule
{
    public static void AddShopsModule(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<CshopDbContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("cshop"));
        });

        builder.Host.ConfigureContainer<ContainerBuilder>((_, builder) =>
        {
            builder.RegisterModule<ShopsModuleAutofac>();
        });
    }

    public static void UseShopsModule(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<CshopDbContext>();
        dbContext.Database.Migrate();
    }
}