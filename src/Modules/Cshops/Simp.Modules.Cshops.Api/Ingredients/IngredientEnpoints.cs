using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Simp.Modules.Cshops.Infrastructure;
using Simp.Modules.Cshops.UseCases.Ingredients.Commands;
using Simp.Modules.Cshops.UseCases.Ingredients.Queries;
using Simp.Shared.Abstractions.Routing;

namespace Simp.Modules.Cshops.Api.Ingredients;
internal class IngredientEnpoints : IEndpointsDefinition
{
    public static void ConfigureEndpoints(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/ingredients", async (ICshopsCompositionRoot compositionRoot) 
            => await compositionRoot.ExecuteAsync(new GetIngredientsQuery()));

        app.MapPost("/api/ingredients", async ([FromBody] CreateIngredientCommand command , ICshopsCompositionRoot compositionRoot) 
            => await compositionRoot.ExecuteAsync(command));
    }
}
