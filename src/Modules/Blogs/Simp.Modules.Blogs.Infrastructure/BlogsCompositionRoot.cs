using Autofac;

namespace Simp.Modules.Blogs.Infrastructure;

public class BlogsCompositionRoot(IContainer container)
{
    public ILifetimeScope GetLifetimeScope() => container.BeginLifetimeScope();
}
