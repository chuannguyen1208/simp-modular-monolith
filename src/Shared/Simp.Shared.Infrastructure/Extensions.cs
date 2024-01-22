using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Simp.Shared.Infrastructure.Routing;

namespace Simp.Shared.Infrastructure;

internal static class Extensions
{
    public static void AddInfrastructure(this WebApplicationBuilder builder)
    {
        builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
    }

    public static void UseInfrastructure(this WebApplication app)
    {
        app.UseEndpoints();
    }
}
