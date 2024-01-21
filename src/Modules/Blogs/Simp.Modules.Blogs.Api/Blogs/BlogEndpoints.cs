using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Simp.Shared.Abstractions.Routing;

namespace Simp.Modules.Blogs.Api.Blogs;
internal class BlogEndpoints : IEndpointsDefinition
{
    public static void ConfigureEndpoints(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/blogs", () => "Blogs");
    }
}
