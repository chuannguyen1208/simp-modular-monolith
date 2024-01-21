using Autofac;
using MediatR;
using Simp.Shared.Abstractions.Compositions;
using Simp.Shared.Abstractions.Mediators;

namespace Simp.Shared.Infrastructure.Mediators;
public abstract class MediatorHandler(ICompositionRoot compositionRoot) : IMediatorHandler
{
    public async Task ExecuteAsync(IRequest request)
    {
        using var scope = compositionRoot.GetLifetimeScope();
        var mediator = scope.Resolve<IMediator>();
        await mediator.Send(request);
    }

    public async Task<TResult> ExecuteAsync<TResult>(IRequest<TResult> request)
    {
        using var scope = compositionRoot.GetLifetimeScope();
        var mediator = scope.Resolve<IMediator>();
        return await mediator.Send(request);
    }
}
