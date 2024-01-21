using Autofac;

namespace Simp.Shared.Abstractions.Compositions;
public interface ICompositionRoot
{
    ILifetimeScope GetLifetimeScope();
}
