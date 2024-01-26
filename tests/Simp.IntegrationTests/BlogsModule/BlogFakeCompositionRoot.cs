using Autofac;
using Microsoft.EntityFrameworkCore;
using Simp.Modules.Blogs.Infrastructure;
using Simp.Modules.Blogs.Infrastructure.EF;

namespace Simp.IntegrationTests.BlogsModule;

public class BlogFakeCompositionRoot : BlogsCompositionRoot
{
    protected override ContainerBuilder ConfigureContainerBuilder()
    {
        var builder = base.ConfigureContainerBuilder();

        var dbContextOptions = new DbContextOptionsBuilder<BlogsDbContext>()
           .UseInMemoryDatabase("blogs-integration")
           .Options;

        builder.RegisterInstance(dbContextOptions).SingleInstance();

        return builder;
    }
}
