using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Simp.Modules.Blogs.Infrastructure;
using Simp.Modules.Blogs.Infrastructure.EF;

namespace Simp.Modules.Blogs.Api;

public static class Extensions
{
    public static void AddBlogsModule(this WebApplicationBuilder builder)
    {
        builder.Host.ConfigureContainer<ContainerBuilder>((_, cb) =>
        {
            cb.RegisterType<BlogsCompositionRoot>().SingleInstance();

            var dbContextOptions = new DbContextOptionsBuilder<BlogsDbContext>()
                .UseSqlServer(builder.Configuration.GetConnectionString("blog"))
                .Options;

            cb.RegisterInstance(dbContextOptions).SingleInstance();
        });
    }
}
