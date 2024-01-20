using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Simp.Modules.Blogs.Api;

internal static class EndpointExtensions
{
    internal static RouteHandlerBuilder WithBlogTags(this RouteHandlerBuilder builder) 
        => builder.WithTags("Blogs");
}
