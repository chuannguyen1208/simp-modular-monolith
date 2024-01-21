using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Simp.Modules.Cshops.Infrastructure;
using Simp.Modules.Cshops.UseCases.Ingredients;
using Simp.Shared.Abstractions.Routing;

namespace Simp.Modules.Cshops.Api.Ingredients;
internal class IngredientEnpoints : IEndpointsDefinition
{
    public static void ConfigureEndpoints(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/ingredients", (CshopsCompositionRoot compositionRoot) => compositionRoot.ExecuteAsync(new GetIngredientsQuery()));
    }
}
