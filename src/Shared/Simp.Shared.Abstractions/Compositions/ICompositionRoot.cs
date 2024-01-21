using Autofac;
using MediatR;

namespace Simp.Shared.Abstractions.Compositions;
public interface ICompositionRoot
{
    Task ExecuteAsync(IRequest request);
    Task<TResult> ExecuteAsync<TResult>(IRequest<TResult> request);
    IContainer ConfigureContainer();
    ILifetimeScope GetLifetimeScope();
}
