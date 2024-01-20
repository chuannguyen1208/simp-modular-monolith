using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Simp.Shared.Infrastructure.Routing;

namespace Simp.Shared.Infrastructure;

internal static class Extensions
{
    public static void AddInfrastructure(this WebApplicationBuilder builder)
    {
        builder.Services.AddSwaggerGen();
        builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
            .ConfigureContainer<ContainerBuilder>((_, builder) =>
            {
                builder.RegisterModule<MediatorModule>();
            });
    }

    public static void UseInfrastructure(this WebApplication app)
    {
        app.UseEndpoints();
        app.UseSwagger().UseSwaggerUI();
    }
}
