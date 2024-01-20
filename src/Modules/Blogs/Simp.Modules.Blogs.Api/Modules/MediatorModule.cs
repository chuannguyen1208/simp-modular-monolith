using Autofac;
using Simp.Modules.Blogs.Api.Queries;

namespace Simp.Modules.Blogs.Api.Modules;

internal class MediatorModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<MediatorHandler>().AsImplementedInterfaces().SingleInstance();
    }
}