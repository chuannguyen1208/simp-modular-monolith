using MediatR;
using Simp.Modules.Shops.Contracts.Ingredients;

namespace Simp.Modules.Shops.UseCases.Ingredients.Queries;
public record GetIngredientQuery(Guid Id) : IRequest<IngredientResponse>
{
    private class Handler : IRequestHandler<GetIngredientQuery, IngredientResponse>
    {
        public async Task<IngredientResponse> Handle(GetIngredientQuery request, CancellationToken cancellationToken)
        {
            await Task.Delay(500, cancellationToken);

            return new IngredientResponse
            {
                Name = "Name",
                StockName = "Stock name"
            };
        }
    }
}
