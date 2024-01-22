using Autofac;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Simp.Modules.Blogs.Infrastructure.EF;
using Simp.Shared.Infrastructure.Repositories;

namespace Simp.Modules.Blogs.Infrastructure.AutofacModules;

internal class DbContextModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        var dbContextOptions = new DbContextOptionsBuilder<BlogsDbContext>()
            .UseSqlServer(configuration.GetConnectionString("blog"))
            .Options;

        builder.RegisterInstance(dbContextOptions).SingleInstance();
        builder.RegisterType<BlogsDbContext>().InstancePerLifetimeScope();

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