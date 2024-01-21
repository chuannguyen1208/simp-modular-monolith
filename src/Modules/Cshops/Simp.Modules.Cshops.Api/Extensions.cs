using Autofac;
using Microsoft.AspNetCore.Builder;
using Simp.Modules.Cshops.Infrastructure;

namespace Simp.Modules.Cshops.Api;
public static class Extensions
{
    public static void AddCshopsModule(this WebApplicationBuilder builder)
    {
        builder.Host.ConfigureContainer<ContainerBuilder>((_, cb) =>
        {
            cb.RegisterType<CshopsCompositionRoot>().SingleInstance();
        });
    }
}
