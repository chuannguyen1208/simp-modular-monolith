using Autofac;
using Simp.Modules.Blogs.Contracts.Blogs;
using Simp.Modules.Blogs.Domain.Blogs;
using Simp.Modules.Blogs.Infrastructure;
using Simp.Modules.Blogs.Infrastructure.EF;

namespace Simp.IntegrationTests.BlogsModule.Blogs;

public class GetBlogsTests(BootstrapperWebApplicationFactory<Program> factory) : IClassFixture<BootstrapperWebApplicationFactory<Program>>
{
    [Fact]
    public async Task GetBlogs()
    {
        // Arrange
        await Initial();

        var client = factory.CreateClient();

        // Act
        var response = await client.GetAsync("/api/blogs");

        // Assert
        response.EnsureSuccessStatusCode(); // Status Code 200-299

        var blogs = await response.Content.ReadFromJsonAsync<IEnumerable<BlogResponse>>();

        Assert.True(blogs?.Any());
        Assert.Equal(2, blogs?.Count());

        // Domain event handlers
        Assert.True(blogs?.All(b => b.Published));
    }

    private async Task Initial()
    {
        using var scope = factory.Services.CreateScope();

        var compositionRoot = scope.ServiceProvider.GetRequiredService<IBlogsCompositionRoot>();

        using var compositionRootScope = compositionRoot.GetLifetimeScope();

        var dbContext = compositionRootScope.Resolve<BlogsDbContext>();

        if (!dbContext.Blogs.Any())
        {
            dbContext.Blogs.AddRange(
                Blog.Create(title: "Blog 1", description: "D1", content: "C1", false),
                Blog.Create(title: "Blog 2", description: "D2", content: "C2", false)
            );

            await dbContext.SaveChangesAsync();
        }
    }
}
