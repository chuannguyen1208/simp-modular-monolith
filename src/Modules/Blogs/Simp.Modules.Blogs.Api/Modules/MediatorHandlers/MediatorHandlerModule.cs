using Autofac;
using Simp.Modules.Blogs.Api.Queries;

namespace Simp.Modules.Blogs.Api.Modules.MediatorHandlers;

internal class MediatorHandlerModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<MediatorHandler>().AsImplementedInterfaces().SingleInstance();
    }
}