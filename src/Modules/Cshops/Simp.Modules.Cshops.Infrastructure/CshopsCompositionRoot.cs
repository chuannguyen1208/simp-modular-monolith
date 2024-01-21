using Autofac;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Simp.Modules.Cshops.Infrastructure.EF;
using Simp.Modules.Cshops.Infrastructure.Services;
using Simp.Modules.Cshops.UseCases.Ingredients.Queries;
using Simp.Shared.Abstractions.Compositions;
using Simp.Shared.Infrastructure.Compositions;
using Simp.Shared.Infrastructure.Repositories;

namespace Simp.Modules.Cshops.Infrastructure;

public interface ICshopsCompositionRoot : ICompositionRoot;

public class CshopsCompositionRoot : CompositionRoot, ICshopsCompositionRoot
{
    public override IContainer ConfigureContainer()
    {
        var builder = new ContainerBuilder();

        builder.RegisterModule(new MediatorModule(typeof(GetIngredientsQuery).Assembly));
        builder.RegisterType<CshopsMessageService>().AsImplementedInterfaces().SingleInstance();

        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        var dbContextOptions = new DbContextOptionsBuilder<CshopDbContext>()
            .UseSqlServer(configuration.GetConnectionString("cshop"))
            .Options;

        builder.RegisterInstance(dbContextOptions).SingleInstance();

        builder.RegisterType<CshopDbContext>().InstancePerLifetimeScope();

        builder.RegisterGeneric(typeof(Repository<>))
            .AsImplementedInterfaces()
            .WithParameter(
                parameterSelector: (pi, ctx) => pi.ParameterType == typeof(DbContext),
                valueProvider: (pi, ctx) => ctx.Resolve<CshopDbContext>())
            .InstancePerLifetimeScope();

        return builder.Build();
    }
}
