using Autofac;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        var response = await client.PostAsJsonAsync("/api/blogs", new CreateBlogCommand("Title", "Description", "This is a text with some *emphasis*", false, false));

        var blogId = await response.Content.ReadFromJsonAsync<Guid>();

        using var scope = _compositionRoot.GetLifetimeScope();

        var dbContext = scope.Resolve<BlogsDbContext>();

        var blog = await dbContext.Blogs.FindAsync(blogId);

        // Assert
        response.EnsureSuccessStatusCode();

        Assert.NotNull(blog);
        Assert.Equal("Title", blog.Title);
        Assert.Equal("Description", blog.Description);
        Assert.Equal("This is a text with some *emphasis*", blog.Content);
        Assert.Equal("<p>This is a text with some <em>emphasis</em></p>\n", blog.ContentHtml);
    }

    [Fact]
    public async Task Create_EmptyTitle_ValidationErrors()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.PostAsJsonAsync("/api/blogs", new CreateBlogCommand(string.Empty, string.Empty, "Content", false, false));

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        var problem = await response.Content.ReadFromJsonAsync<ProblemDetails>();

        Assert.NotNull(problem);
    }

    [Fact]
    public async Task Edit_Ok()
    {
        using var scope = _compositionRoot.GetLifetimeScope();

        var context = scope.Resolve<BlogsDbContext>();

        var blog = context.Blogs.AsNoTracking().First();

        var client = _factory.CreateClient();

        var response = await client.PutAsJsonAsync($"/api/blogs/{blog.Id}", new UpdateBlogCommand(
            Guid.Empty, 
            "Title update", 
            "Description update", 
            "This is a text with some *emphasis*",
            false,
            false));

        response.EnsureSuccessStatusCode();

        blog = context.Blogs.AsNoTracking().First(s => s.Id == blog.Id);

        Assert.Equal("Title update", blog.Title);
        Assert.Equal("Description update", blog.Description);
        Assert.Equal("This is a text with some *emphasis*", blog.Content);
        Assert.Equal("<p>This is a text with some <em>emphasis</em></p>\n", blog.ContentHtml);
    }

    [Fact]
    public async Task Edit_EmptyTitle_ValidationError()
    {
        using var scope = _compositionRoot.GetLifetimeScope();

        var context = scope.Resolve<BlogsDbContext>();

        var blog = context.Blogs.AsNoTracking().First();

        var client = _factory.CreateClient();

        var response = await client.PutAsJsonAsync($"/api/blogs/{blog.Id}", new UpdateBlogCommand(
            Guid.Empty, 
            string.Empty, 
            "Description update", 
            "Content update",
            false,
            false));

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        var problem = await response.Content.ReadFromJsonAsync<ProblemDetails>();

        Assert.NotNull(problem);
    }

    [Fact]
    public async Task Delete_Ok()
    {
        using var scope = _compositionRoot.GetLifetimeScope();

        var context = scope.Resolve<BlogsDbContext>();

        var blog = context.Blogs.AsNoTracking().First();

        var client = _factory.CreateClient();

        var response = await client.DeleteAsync($"/api/blogs/{blog.Id}");

        response.EnsureSuccessStatusCode();

        blog = context.Blogs.AsNoTracking().FirstOrDefault(s => s.Id == blog.Id);

        Assert.Null(blog);
    }
}
