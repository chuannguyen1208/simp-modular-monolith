using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Simp.Modules.Shops.UseCases.Ingredients.Queries;
using Simp.Shared.Abstractions.Routing;

namespace Simp.Modules.Shops.Api.Ingredients;
internal class IngredientEndpoints : IEndpointsDefinition
{
    public static void ConfigureEndpoints(IEndpointRouteBuilder app)
    {
        app
            .MapGet("/api/ingredients", (IMediator mediator) => mediator.Send(new GetInrgedientsQuery()))
            .WithIngredientTags();

        app
            .MapGet("/api/ingredients/{id:guid}", (Guid id, IMediator mediator) => mediator.Send(new GetIngredientQuery(id)))
            .WithIngredientTags();
    }
}
