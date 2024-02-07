using Autofac;
using Microsoft.EntityFrameworkCore;
using Simp.Modules.Cshops.Infrastructure.EF;
using Simp.Modules.Cshops.Infrastructure.Services;
using Simp.Shared.Infrastructure.Repositories;

namespace Simp.Modules.Cshops.Infrastructure.AutofacModules;
internal class ServicesModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<CshopsMessageService>().AsImplementedInterfaces().SingleInstance();
        builder.RegisterGeneric(typeof(Repository<>))
            .AsImplementedInterfaces()
            .WithParameter(
                parameterSelector: (pi, ctx) => pi.ParameterType == typeof(DbContext),
                valueProvider: (pi, ctx) => ctx.Resolve<CshopDbContext>())
            .InstancePerLifetimeScope();
    }
}