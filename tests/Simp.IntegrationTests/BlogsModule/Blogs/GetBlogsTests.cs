using Autofac;
using Simp.Modules.Blogs.Contracts.Blogs;
using Simp.Modules.Blogs.Domain.Blogs;
using Simp.Modules.Blogs.Infrastructure;
using Simp.Modules.Blogs.Infrastructure.EF;

namespace Simp.IntegrationTests.BlogsModule.Blogs;

public class GetBlogsTests(BootstrapperWebApplicationFactory<Program> factory) : BlogsTestBase(factory)
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
}
