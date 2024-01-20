using MediatR;

namespace Simp.Shared.Abstractions.Mediators;
public interface IMediatorHandler
{
    Task ExecuteAsync(IRequest request);
    Task<TResult> ExecuteAsync<TResult>(IRequest<TResult> request);
}
