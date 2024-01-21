using Autofac;
using Microsoft.AspNetCore.Builder;
using Simp.Modules.Cshops.Api.Modules.Compositions;

namespace Simp.Modules.Cshops.Api;

public static class Extensions
{
    public static void AddCshopModules(this WebApplicationBuilder builder)
    {
        builder.Host.ConfigureContainer<ContainerBuilder>((_, cb) =>
        {
            cb.RegisterModule<CompositionModule>();
        });
    }
}
