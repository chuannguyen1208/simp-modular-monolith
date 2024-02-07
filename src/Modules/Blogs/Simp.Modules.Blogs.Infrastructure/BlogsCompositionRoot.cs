using Autofac;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Simp.Modules.Blogs.Infrastructure.AutofacModules;
using Simp.Modules.Blogs.UseCases.Blogs.Queries;
using Simp.Shared.Abstractions.Compositions;
using Simp.Shared.Infrastructure.Compositions;

namespace Simp.Modules.Blogs.Infrastructure;

public interface IBlogsCompositionRoot : ICompositionRoot;

public class BlogsCompositionRoot(IConfiguration configuration) : CompositionRoot, IBlogsCompositionRoot
{
    protected override ContainerBuilder ConfigureContainerBuilder()
    {
        var builder = new ContainerBuilder();

        var connectionString = configuration.GetConnectionString("blog") ?? "";

        builder.RegisterModule(new DbContextModule(connectionString));
        builder.RegisterModule<ServicesModule>();

        builder.RegisterModule(new MediatorModule(typeof(GetBlogsQuery).Assembly));
        builder.RegisterModule<AutoMapperModule>();

        return builder;
    }
}
