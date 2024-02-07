using Autofac;
using Simp.Modules.Blogs.Domain.Blogs;
using Simp.Modules.Blogs.Infrastructure;
using Simp.Modules.Blogs.Infrastructure.EF;

namespace Simp.IntegrationTests.BlogsModule;

public class BlogsTestBase : IClassFixture<BootstrapperWebApplicationFactory<Program>>
{
    protected BootstrapperWebApplicationFactory<Program> _factory;
    protected IBlogsCompositionRoot _compositionRoot;

    public BlogsTestBase(BootstrapperWebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _compositionRoot = factory.Services.GetRequiredService<IBlogsCompositionRoot>();

        Initial();
    }

    private void Initial()
    {
        using var scope = _compositionRoot.GetLifetimeScope();
        var context = scope.Resolve<BlogsDbContext>();

        if (!context.Blogs.Any())
        {
            context.Blogs.Add(Blog.Create("BLog", "BlogD", "BlogC", "BlogC"));

            var template = Blog.Create("Template", "Template", "Template", "Template");

            template.UpdateIsTemplate(true);

            context.Blogs.Add(template);

            context.SaveChanges();
        }
    }

}
