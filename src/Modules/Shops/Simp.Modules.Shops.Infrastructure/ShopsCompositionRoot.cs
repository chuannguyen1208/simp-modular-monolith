using Autofac;
using Simp.Modules.Shops.UseCases;

namespace Simp.Modules.Shops.Infrastructure;

internal class ShopsCompositionRoot(IContainer container) : IShopsCompositionRoot
{
    private readonly IContainer _container = container;

    public ILifetimeScope BeginLifetimeScope()
        => _container.BeginLifetimeScope();
}
