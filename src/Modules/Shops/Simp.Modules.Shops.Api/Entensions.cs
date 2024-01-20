using Microsoft.AspNetCore.Builder;
using Simp.Modules.Shops.Infrastructure;
using Simp.Modules.Shops.UseCases;

namespace Simp.Modules.Shops.Api;

internal static class Entensions
{
    public static void AddShopsModule(this WebApplicationBuilder builder)
    {
        builder.AddUseCases();
        builder.AddInfrastructure();
    }

    public static void UseShopsModule(this WebApplication app)
    {
    }
}