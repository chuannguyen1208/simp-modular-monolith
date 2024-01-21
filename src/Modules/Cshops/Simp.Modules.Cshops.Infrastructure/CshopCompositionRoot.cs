using Autofac;
using Simp.Shared.Infrastructure.Compositions;

namespace Simp.Modules.Cshops.Infrastructure;
internal class CshopCompositionRoot(IContainer container) : CompositionRoot(container)
{
}
