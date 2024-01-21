using MediatR;
using Simp.Shared.Abstractions.Services;

namespace Simp.Modules.Cshops.UseCases.Ingredients;
public class GetIngredientsQuery : IRequest<string>
{
    private class Handler(IMessageService messageService) : IRequestHandler<GetIngredientsQuery, string>
    {
        public async Task<string> Handle(GetIngredientsQuery request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(messageService.SayHello());
        }
    }
}
