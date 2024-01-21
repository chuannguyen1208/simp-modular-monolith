using Autofac;
using Simp.Modules.Blogs.Api.Services;

namespace Simp.Modules.Blogs.Api.Modules.Compositions;

internal class InfrastructureModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<MessageService>()
            .AsImplementedInterfaces()
            .SingleInstance();
    }
}