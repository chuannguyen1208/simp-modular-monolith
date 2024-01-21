using MediatR;
using Simp.Modules.Cshop.Domain.Entities;
using Simp.Shared.Abstractions.Repositories;

namespace Simp.Modules.Cshops.UseCases.Ingredients.Commands;
public record CreateIngredientCommand(string Name, string StockName, int StockLevel) : IRequest<Guid>
{
    private class Handler(IRepository<Ingredient> repository) : IRequestHandler<CreateIngredientCommand, Guid>
    {
        public async Task<Guid> Handle(CreateIngredientCommand request, CancellationToken cancellationToken)
        {
            var entity = Ingredient.Create(request.Name, request.StockName, request.StockLevel);
            await repository.CreateAsync(entity);
            await repository.SaveChangesAsync();

            return entity.Id;
        }
    }
}
