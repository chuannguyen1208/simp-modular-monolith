using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Simp.Modules.Blogs.Infrastructure;
using Simp.Modules.Blogs.UseCases.Blogs.Commands;
using Simp.Modules.Blogs.UseCases.Blogs.Queries;
using Simp.Shared.Abstractions.Routing;

namespace Simp.Modules.Blogs.Api.Blogs;
internal class BlogEndpoints : IEndpointsDefinition
{
    public static void ConfigureEndpoints(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/blogs", async (IBlogsCompositionRoot compositionRoot) => await compositionRoot.ExecuteAsync(new GetBlogsQuery()));

        app.MapGet("/api/blogs/{id}", async (Guid id, IBlogsCompositionRoot compositionRoot) =>
        {
            var res = await compositionRoot.ExecuteAsync(new GetBlogQuery(id));

            if (res is null)
            {
                return Results.NotFound($"Entity with ID {id} not found");
            }

            return Results.Ok(res);
        });

        app.MapPost("/api/blogs", async (CreateBlogCommand command, IBlogsCompositionRoot compositionRoot) => await compositionRoot.ExecuteAsync(command));

        app.MapPut("/api/blogs/{id}", async (Guid id, UpdateBlogCommand command, IBlogsCompositionRoot compositionRoot) =>
        {
            var updateCommand = command with { Id = id };

            await compositionRoot.ExecuteAsync(updateCommand);

            return Results.NoContent();
        });

        app.MapDelete("/api/blogs/{id}", async (Guid id, IBlogsCompositionRoot compositionRoot) =>
        {
            var command = new DeleteBlogCommand(id);

            await compositionRoot.ExecuteAsync(command);

            return Results.NoContent();
        });
    }
}
