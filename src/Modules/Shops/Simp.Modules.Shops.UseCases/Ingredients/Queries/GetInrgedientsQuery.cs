using Autofac;
using MediatR;
using Simp.Modules.Shops.Contracts.Ingredients;
using Simp.Modules.Shops.Domain.Entities;
using Simp.Shared.Abstractions.Repositories;

namespace Simp.Modules.Shops.UseCases.Ingredients.Queries;
public record GetInrgedientsQuery : IRequest<IEnumerable<IngredientResponse>>
{
    private class Handler(IShopsCompositionRoot compositionRoot) : IRequestHandler<GetInrgedientsQuery, IEnumerable<IngredientResponse>>
    {
        public async Task<IEnumerable<IngredientResponse>> Handle(GetInrgedientsQuery request, CancellationToken cancellationToken)
        {
            using var scope = compositionRoot.BeginLifetimeScope();
            
            var unitOfWork = scope.Resolve<IUnitOfWork>();
            var repository = unitOfWork.GetRepository<Ingredient>();
            var entities = await repository.GetAllAsync();

            var res = entities.Select(s => new IngredientResponse
            {
                Id = s.Id,
                Name = s.Name,
                StockName = s.StockName,
                StockLevel = s.StockLevel,
            });

            return res;
        }
    }
}
