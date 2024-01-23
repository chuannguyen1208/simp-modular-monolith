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
        builder.Host
            .ConfigureContainer<ContainerBuilder>((_, cb) =>
            {
                cb.RegisterType<CshopsCompositionRoot>().As<ICshopsCompositionRoot>().SingleInstance();

                var dbContextOptions = new DbContextOptionsBuilder<CshopDbContext>()
                    .UseSqlServer(builder.Configuration.GetConnectionString("cshop"))
                    .Options;

                cb.RegisterInstance(dbContextOptions).SingleInstance();
            });
    }

    public static void UseCshopsModule(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var compositionRoot = scope.ServiceProvider.GetRequiredService<ICshopsCompositionRoot>();

        var compositionScope = compositionRoot.GetLifetimeScope();

        var dbContext = compositionScope.Resolve<CshopDbContext>();

        if (dbContext.Database.IsRelational())
        {
            dbContext.Database.Migrate();
        }
    }
}
