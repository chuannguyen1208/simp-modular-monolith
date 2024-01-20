using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Simp.Modules.Shops.Infrastructure.EF;
using Simp.Modules.Shops.UseCases;
using Simp.Shared.Abstractions.Repositories;
using Simp.Shared.Infrastructure.Repositories;

namespace Simp.Modules.Shops.Infrastructure;
public static class Extensions
{
    public static void AddInfrastructure(this WebApplicationBuilder builder)
    {
        var container = CreateContainer(
            connectionString: builder.Configuration.GetConnectionString("cshop")!);

        builder.Host.ConfigureContainer<ContainerBuilder>((_, builder) =>
        {
            builder
            .RegisterInstance(new ShopsCompositionRoot(container))
            .As<IShopsCompositionRoot>()
            .SingleInstance();
        });
    }

    private static IContainer CreateContainer(string connectionString)
    {
        var containerBuilder = new ContainerBuilder();

        containerBuilder.Register(c =>
            {
                var dbContextOptionsBuilder = new DbContextOptionsBuilder<CshopDbContext>();
                dbContextOptionsBuilder.UseSqlServer(connectionString);
                return dbContextOptionsBuilder.Options;
            })
            .As<DbContextOptions<CshopDbContext>>()
            .SingleInstance();

        containerBuilder
            .RegisterType<CshopDbContext>()
            .InstancePerLifetimeScope();

        containerBuilder
            .RegisterType<UnitOfWork>()
            .As<IUnitOfWork>()
            .WithParameter(
                parameterSelector: (pi, ctx) => pi.ParameterType == typeof(DbContext),
                valueProvider: (pi, ctx) => ctx.Resolve<CshopDbContext>())
            .InstancePerLifetimeScope();

        return containerBuilder.Build();
    }
}
