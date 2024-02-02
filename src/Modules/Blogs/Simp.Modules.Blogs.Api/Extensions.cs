using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Simp.Modules.Blogs.Infrastructure;
using Simp.Modules.Blogs.Infrastructure.EF;

namespace Simp.Modules.Blogs.Api;

public static class Extensions
{
    public static void AddBlogsModule(this WebApplicationBuilder builder)
    {
        builder.Host.ConfigureContainer<ContainerBuilder>((_, cb) =>
        {
            cb.RegisterType<BlogsCompositionRoot>().As<IBlogsCompositionRoot>().SingleInstance();

            var dbContextOptions = new DbContextOptionsBuilder<BlogsDbContext>()
                .UseSqlServer(builder.Configuration.GetConnectionString("blog"))
                .Options;

            cb.RegisterInstance(dbContextOptions).SingleInstance();
        });
    }

    public static void UseBlogsModule(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        
        var compositionRoot = scope.ServiceProvider.GetRequiredService<IBlogsCompositionRoot>();
        
        using var compositionScope = compositionRoot.GetLifetimeScope();

        var dbContext = compositionScope.Resolve<BlogsDbContext>();

        Console.WriteLine("Blog connection string: " + dbContext.Database.GetConnectionString());

        if (dbContext.Database.IsRelational())
        {
            dbContext.Database.Migrate();
        }
    }
}
