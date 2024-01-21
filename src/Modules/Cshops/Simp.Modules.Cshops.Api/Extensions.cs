using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Simp.Modules.Cshops.Infrastructure;
using Simp.Modules.Cshops.Infrastructure.EF;

namespace Simp.Modules.Cshops.Api;
public static class Extensions
{
    public static void AddCshopsModule(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<CshopDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("cshop")));

        builder.Host.ConfigureContainer<ContainerBuilder>((_, cb) =>
        {
            cb.RegisterType<CshopsCompositionRoot>().SingleInstance();
        });
    }

    public static void UseCshopsModule(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<CshopDbContext>();
        dbContext.Database.Migrate();
    }
}
