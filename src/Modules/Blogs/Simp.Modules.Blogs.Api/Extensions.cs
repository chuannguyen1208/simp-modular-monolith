using Microsoft.AspNetCore.Builder;
using Simp.Modules.Blogs.UseCases;

namespace Simp.Modules.Blogs.Api;
public static class Extensions
{
    public static void AddBlogsModule(this WebApplicationBuilder builder)
    {
        builder.AddUseCases();
    }

    public static void UseBlogsModule(this WebApplication app)
    {
    }
}
