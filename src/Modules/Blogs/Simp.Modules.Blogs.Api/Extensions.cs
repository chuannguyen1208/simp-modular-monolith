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
        // Add this for migration only
        builder.Services.AddDbContext<BlogsDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("blog")));

        builder.Host.ConfigureContainer<ContainerBuilder>((_, cb) =>
        {
            cb.RegisterType<BlogsCompositionRoot>().SingleInstance();
        });
    }
}
