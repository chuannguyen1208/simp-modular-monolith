using Autofac;
using Simp.Modules.Blogs.Infrastructure.EF;
using Simp.Modules.Blogs.UseCases.Blogs.Commands;
using System.Net;

namespace Simp.IntegrationTests.BlogsModule.Blogs;

public class BlogCommandTests(BootstrapperWebApplicationFactory<Program> factory) : BlogsTestBase(factory)
{
    [Fact]
    public async Task Create_Ok()
    {
        // Arrage
        var client = _factory.CreateClient();

        // Act
        var response = await client.PostAsJsonAsync<CreateBlogCommand>("/api/blogs", new CreateBlogCommand("Title", "Description", "Content"));

        var blogId = await response.Content.ReadFromJsonAsync<Guid>();

        using var scope = _compositionRoot.GetLifetimeScope();
        
        var dbContext = scope.Resolve<BlogsDbContext>();

        var blog = await dbContext.Blogs.FindAsync(blogId);

        // Assert
        response.EnsureSuccessStatusCode();

        Assert.NotNull(blog);
        Assert.Equal("Title", blog.Title);
        Assert.Equal("Description", blog.Description);
        Assert.Equal("Content", blog.Content);
    }

    [Fact]
    public async Task Create_EmptyTitle_ValidationErrors()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.PostAsJsonAsync<CreateBlogCommand>("/api/blogs", new CreateBlogCommand(string.Empty, "Description", "Content"));

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}
