using Autofac;
using MediatR;
using Simp.Shared.Abstractions.Compositions;

namespace Simp.Shared.Infrastructure.Compositions;
public class CompositionRoot : ICompositionRoot
{
    protected readonly IContainer _container;

    public CompositionRoot()
    {
        _container = ConfigureContainer();
    }

    public async Task ExecuteAsync(IRequest request)
    {
        using var scope = _container.BeginLifetimeScope();

        var mediator = scope.Resolve<IMediator>();

        await mediator.Send(request);
    }

    public async Task<TResult> ExecuteAsync<TResult>(IRequest<TResult> request)
    {
        using var scope = _container.BeginLifetimeScope();

        var mediator = scope.Resolve<IMediator>();

        return await mediator.Send(request);
    }

    public virtual IContainer ConfigureContainer()
    {
        return new ContainerBuilder().Build();
    }
}
