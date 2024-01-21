using Autofac;
using Simp.Shared.Abstractions.Compositions;

namespace Simp.Shared.Infrastructure.Compositions;
public abstract class CompositionRoot(IContainer container) : ICompositionRoot
{
    public ILifetimeScope GetLifetimeScope()
        => container.BeginLifetimeScope();
}
