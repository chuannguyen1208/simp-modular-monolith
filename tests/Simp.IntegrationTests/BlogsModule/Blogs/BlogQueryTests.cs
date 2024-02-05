using Autofac;
using Microsoft.EntityFrameworkCore;
using Simp.Modules.Blogs.Contracts.Blogs;
using Simp.Modules.Blogs.Infrastructure.EF;
using System.Net;

namespace Simp.IntegrationTests.BlogsModule.Blogs;

public class BlogQueryTests(BootstrapperWebApplicationFactory<Program> factory) : BlogsTestBase(factory)
{
    [Fact]
    public async Task GetBlogs()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/api/blogs");

        // Assert
        response.EnsureSuccessStatusCode(); // Status Code 200-299

        var blogs = await response.Content.ReadFromJsonAsync<IEnumerable<BlogResponse>>();

        Assert.True(blogs?.Any());
        Assert.True(blogs?.All(b => b.Published));
    }

    [Fact]
    public async Task GetBlog()
    {
        // Arrange
        using var scope = _compositionRoot.GetLifetimeScope();

        var context = scope.Resolve<BlogsDbContext>();

        var entity = await context.Blogs.FirstAsync();

        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync($"/api/blogs/{entity.Id}");

        var blog = await response.Content.ReadFromJsonAsync<BlogResponse>();

        // Assert
        Assert.NotNull(blog);
    }

    [Fact]
    public async Task GetBlog_NotFound()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync($"/api/blogs/{Guid.NewGuid()}");

        // Assert
        response.StatusCode.Equals(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetBlogTemplates()
    {
        var client = _factory.CreateClient();

        var response = await client.GetFromJsonAsync<IEnumerable<BlogResponse>>("/api/blogs/templates");

        Assert.True(response!.Any());
    }
}
