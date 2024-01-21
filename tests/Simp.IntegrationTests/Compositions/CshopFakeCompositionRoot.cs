using Autofac;
using Microsoft.EntityFrameworkCore;
using Simp.Modules.Cshops.Infrastructure;
using Simp.Modules.Cshops.Infrastructure.EF;
using Simp.Modules.Cshops.UseCases.Ingredients;
using Simp.Shared.Infrastructure.Compositions;
using Simp.Shared.Infrastructure.Repositories;

namespace Simp.IntegrationTests.Compositions;

public class CshopsFakeCompositionRoot : CshopsCompositionRoot
{
    public override IContainer ConfigureContainer()
    {
        var builder = new ContainerBuilder();

        builder.RegisterModule(new MediatorModule(typeof(GetIngredientsQuery).Assembly));
        //builder.RegisterType<CshopsMessageService>().AsImplementedInterfaces().SingleInstance();

        var dbContextOptions = new DbContextOptionsBuilder<CshopDbContext>()
            .UseInMemoryDatabase("cshop-integration")
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