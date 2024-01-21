using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
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
            });
    }

    public static void UseCshopsModule(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var compositionRoot = scope.ServiceProvider.GetRequiredService<ICshopsCompositionRoot>();
        var compositionScope = compositionRoot.GetLifetimeScope();
        var dbContext = compositionScope.Resolve<CshopDbContext>();
        dbContext.Database.Migrate();
    }
}
