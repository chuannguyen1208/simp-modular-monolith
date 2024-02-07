using Autofac;
using Simp.Modules.Blogs.Infrastructure;

namespace Simp.IntegrationTests.BlogsModule;

public class BlogFakeCompositionRoot(IConfiguration configuration) : BlogsCompositionRoot(configuration)
{
    protected override ContainerBuilder ConfigureContainerBuilder()
    {
        var builder = base.ConfigureContainerBuilder();
        return builder;
    }
}
