using Autofac;
using Simp.Shared.Infrastructure.Compositions;

namespace Simp.Modules.Blogs.Infrastructure;

public class BlogsCompositionRoot(IContainer container) : CompositionRoot(container)
{
}
