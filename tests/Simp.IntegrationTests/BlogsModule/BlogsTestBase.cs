using Autofac;
using Simp.Modules.Blogs.Domain.Blogs;
using Simp.Modules.Blogs.Infrastructure;
using Simp.Modules.Blogs.Infrastructure.EF;

namespace Simp.IntegrationTests.BlogsModule;

public class BlogsTestBase : IClassFixture<BootstrapperWebApplicationFactory<Program>>, IDisposable
{
    protected BlogsDbContext _context;
    protected BootstrapperWebApplicationFactory<Program> _factory;

    public BlogsTestBase(BootstrapperWebApplicationFactory<Program> factory)
    {
        _factory = factory;

        using var scope = factory.Services.CreateScope();

        var composition = scope.ServiceProvider.GetRequiredService<IBlogsCompositionRoot>();

        using var compositionScope = composition.GetLifetimeScope();

        _context = compositionScope.Resolve<BlogsDbContext>();

        Initial();
    }

    private void Initial()
    {
        if (!_context.Blogs.Any())
        {
            _context.Blogs.AddRange(
                Blog.Create("B1", "D1", "C1", false)
            );

            _context.SaveChanges();
        }
    }

    public void Dispose() => _context.Dispose();
}
