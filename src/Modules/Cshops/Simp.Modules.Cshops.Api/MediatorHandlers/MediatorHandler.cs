using MediatR;
using Simp.Shared.Abstractions.Mediators;

namespace Simp.Modules.Cshops.Api.MediatorHandlers;
internal class MediatorHandler() : IMediatorHandler
{
    public Task ExecuteAsync(IRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<TResult> ExecuteAsync<TResult>(IRequest<TResult> request)
    {
        throw new NotImplementedException();
    }
}
