using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Simp.Modules.Blogs.Infrastructure;
using Simp.Modules.Blogs.Infrastructure.AutofacModules;
using Simp.Modules.Blogs.Infrastructure.EF;

namespace Simp.Modules.Blogs.Api;

public static class Extensions
{
    public static void AddBlogsModule(this WebApplicationBuilder builder)
    {
        builder.Host.ConfigureContainer<ContainerBuilder>((_, cb) =>
        {
            var connectionString = builder.Configuration.GetConnectionString("blog") ?? "";

            cb.RegisterModule(new DbContextModule(connectionString));
            cb.RegisterType<BlogsCompositionRoot>().As<IBlogsCompositionRoot>().SingleInstance();
        });
    }

    public static void UseBlogsModule(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        
        var dbContext = scope.ServiceProvider.GetRequiredService<BlogsDbContext>();

        if (dbContext.Database.IsRelational())
        {
            dbContext.Database.Migrate();
        }
    }
}
