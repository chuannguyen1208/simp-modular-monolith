using MediatR;
using Simp.Modules.Cshop.Domain.Entities;
using Simp.Shared.Abstractions.Repositories;

namespace Simp.Modules.Cshops.UseCases.Ingredients;
public class GetIngredientsQuery : IRequest<IEnumerable<Ingredient>>
{
    private class Handler(IRepository<Ingredient> repository) : IRequestHandler<GetIngredientsQuery, IEnumerable<Ingredient>>
    {
        public async Task<IEnumerable<Ingredient>> Handle(GetIngredientsQuery request, CancellationToken cancellationToken)
        {
            var entities = await repository.GetAllAsync().ConfigureAwait(false);
            return entities;
        }
    }
}
