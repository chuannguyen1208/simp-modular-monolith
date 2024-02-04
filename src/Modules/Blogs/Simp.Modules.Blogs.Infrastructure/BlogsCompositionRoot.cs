using Autofac;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Serilog;
using Simp.Modules.Blogs.Infrastructure.AutofacModules;
using Simp.Modules.Blogs.Infrastructure.Contents;
using Simp.Modules.Blogs.Infrastructure.EF;
using Simp.Modules.Blogs.UseCases.Blogs.Queries;
using Simp.Shared.Abstractions.Compositions;
using Simp.Shared.Infrastructure.Compositions;
using Simp.Shared.Infrastructure.Repositories;

namespace Simp.Modules.Blogs.Infrastructure;

public interface IBlogsCompositionRoot : ICompositionRoot;

public class BlogsCompositionRoot(IConfiguration configuration) : CompositionRoot, IBlogsCompositionRoot
{
    protected override ContainerBuilder ConfigureContainerBuilder()
    {
        var builder = new ContainerBuilder();

        var connectionString = configuration.GetConnectionString("blog");

        Log.Information($"Blog connection string: {connectionString}");

        var dbContextOptions = new DbContextOptionsBuilder<BlogsDbContext>()
            .UseSqlServer(connectionString)
            .Options;

        builder.RegisterInstance(dbContextOptions).SingleInstance();

        builder.RegisterType<BlogsDbContext>().InstancePerLifetimeScope();

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

        builder.RegisterModule(new MediatorModule(typeof(GetBlogsQuery).Assembly));
        builder.RegisterModule<AutoMapperModule>();

        return builder;
    }
}
