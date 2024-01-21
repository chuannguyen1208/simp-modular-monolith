using Autofac;
using Simp.Modules.Blogs.Infrastructure;

namespace Simp.Modules.Blogs.Api.Modules.Compositions;

internal class CompositionModule : Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        var container = ConfigureContainer();
        var compositionRoot = new BlogsCompositionRoot(container);
        builder.RegisterInstance(compositionRoot).SingleInstance();
    }

    private static IContainer ConfigureContainer()
    {
        var builder = new ContainerBuilder();

        builder.RegisterModule<MediatorModule>();
        builder.RegisterModule<InfrastructureModule>();

        var container = builder.Build();
        return container;
    }
}