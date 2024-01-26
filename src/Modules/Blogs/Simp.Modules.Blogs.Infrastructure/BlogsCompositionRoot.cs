using Autofac;
using Simp.Modules.Blogs.Infrastructure.AutofacModules;
using Simp.Modules.Blogs.UseCases.Blogs.Queries;
using Simp.Shared.Abstractions.Compositions;
using Simp.Shared.Infrastructure.Compositions;

namespace Simp.Modules.Blogs.Infrastructure;

public interface IBlogsCompositionRoot : ICompositionRoot;

public class BlogsCompositionRoot : CompositionRoot, IBlogsCompositionRoot
{
    protected override ContainerBuilder ConfigureContainerBuilder()
    {
        var builder = new ContainerBuilder();

        builder.RegisterModule(new MediatorModule(typeof(GetBlogsQuery).Assembly));
        builder.RegisterModule<DbContextModule>();

        return builder;
    }
}
