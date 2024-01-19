using MediatR;
using Simp.Modules.Shops.Contracts.Ingredients;

namespace Simp.Modules.Shops.UseCases.Ingredients.Queries;
public record GetInrgedientsQuery : IRequest<IEnumerable<IngredientResponse>>
{
    private class Handler : IRequestHandler<GetInrgedientsQuery, IEnumerable<IngredientResponse>>
    {
        public async Task<IEnumerable<IngredientResponse>> Handle(GetInrgedientsQuery request, CancellationToken cancellationToken)
        {
            await Task.Delay(500, cancellationToken);
            return Enumerable.Empty<IngredientResponse>();
        }
    }
}
