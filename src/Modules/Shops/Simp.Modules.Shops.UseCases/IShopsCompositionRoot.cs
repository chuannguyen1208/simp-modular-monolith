using Autofac;

namespace Simp.Modules.Shops.UseCases;
public interface IShopsCompositionRoot
{
    ILifetimeScope BeginLifetimeScope();
}
