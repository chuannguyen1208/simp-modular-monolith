using Autofac;

namespace Simp.Modules.Blogs.Api.Modules.MediatorHandlers;

internal class MediatorHandlerModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<BlogsMediatorHandler>().SingleInstance();
    }
}