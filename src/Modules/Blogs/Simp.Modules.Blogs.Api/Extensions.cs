using Autofac;
using Microsoft.AspNetCore.Builder;
using Simp.Modules.Blogs.Infrastructure;

namespace Simp.Modules.Blogs.Api;

public static class Extensions
{
    public static void AddBlogsModule(this WebApplicationBuilder builder)
    {
        builder.Host.ConfigureContainer<ContainerBuilder>((_, cb) =>
        {
            cb.RegisterType<BlogsCompositionRoot>().SingleInstance();
        });
    }
}
