using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Simp.Shared.Abstractions.Routing;

namespace Simp.Modules.Shops.Api.Ingredients;
internal class IngredientEndpoints : IEndpointsDefinition
{
    public static void ConfigureEndpoints(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/ingredients", () => "Ingredients");
    }
}
