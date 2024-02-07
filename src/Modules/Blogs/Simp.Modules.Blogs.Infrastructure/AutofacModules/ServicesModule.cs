using Autofac;
using Microsoft.EntityFrameworkCore;
using Simp.Modules.Blogs.Infrastructure.Contents;
using Simp.Modules.Blogs.Infrastructure.EF;
using Simp.Shared.Infrastructure.Repositories;

namespace Simp.Modules.Blogs.Infrastructure.AutofacModules;

internal class ServicesModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<ContentProcessor>().AsImplementedInterfaces().SingleInstance();

        builder.RegisterGeneric(typeof(Repository<>))
           .WithParameter(
               parameterSelector: (pi, ctx) => pi.ParameterType == typeof(DbContext),
               valueProvider: (pi, ctx) => ctx.Resolve<BlogsDbContext>())
           .InstancePerLifetimeScope();

        builder.RegisterType<UnitOfWork>()
            .AsImplementedInterfaces()
            .WithParameter(
                 parameterSelector: (pi, ctx) => pi.ParameterType == typeof(DbContext),
                valueProvider: (pi, ctx) => ctx.Resolve<BlogsDbContext>());
    }
}