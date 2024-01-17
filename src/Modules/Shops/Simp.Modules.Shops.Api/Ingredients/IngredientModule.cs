using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Simp.Shared.Abstractions.Routing;

namespace Simp.Modules.Shops.Api.Ingredients;
internal class IngredientModule : IEndpointsDefinition
{
    public static void ConfigureEndpoints(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/ingredient", () => "Cshop ingredient api").WithTags("Cshop.Ingredient");
    }
}
