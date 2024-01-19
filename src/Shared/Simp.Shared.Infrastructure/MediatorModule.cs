using Autofac;
using MediatR;
using System.Reflection;

namespace Simp.Shared.Infrastructure;

internal class MediatorModule : Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly).AsImplementedInterfaces();
    }
}