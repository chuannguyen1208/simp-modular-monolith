using Autofac;
using Microsoft.AspNetCore.Builder;
using Simp.Modules.Blogs.Api.Modules.Compositions;
using Simp.Modules.Blogs.Api.Modules.MediatorHandlers;

namespace Simp.Modules.Blogs.Api;

public static class Extensions
{
    public static void AddBlogsModule(this WebApplicationBuilder builder)
    {
        builder.Host.ConfigureContainer<ContainerBuilder>((_, cb) =>
        {
            cb.RegisterModule<CompositionModule>();
            cb.RegisterModule<MediatorHandlerModule>();
        });
    }
}
