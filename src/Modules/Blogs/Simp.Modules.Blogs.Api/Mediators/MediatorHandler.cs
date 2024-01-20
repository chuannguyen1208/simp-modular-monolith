using Autofac;
using MediatR;
using Simp.Modules.Blogs.Infrastructure;
using Simp.Shared.Abstractions.Mediators;

namespace Simp.Modules.Blogs.Api.Queries;
internal class MediatorHandler(BlogsCompositionRoot compositionRoot) : IMediatorHandler
{
    public async Task ExecuteAsync(IRequest request)
    {
        using var scope = compositionRoot.GetLifetimeScope();
        var meditor = scope.Resolve<IMediator>();
        await meditor.Send(request);
    }

    public async Task<TResult> ExecuteAsync<TResult>(IRequest<TResult> request)
    {
        using var scope = compositionRoot.GetLifetimeScope();
        var meditor = scope.Resolve<IMediator>();
        return await meditor.Send(request);
    }
}
