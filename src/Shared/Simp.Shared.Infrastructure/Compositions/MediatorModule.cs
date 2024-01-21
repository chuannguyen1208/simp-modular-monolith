using Autofac;
using MediatR.Pipeline;
using MediatR;
using System.Reflection;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace Simp.Shared.Infrastructure.Compositions;

public class MediatorModule(Assembly mediatorAssembly) : Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly).AsImplementedInterfaces();

        var mediatrOpenTypes = new[]
        {
                typeof(IRequestHandler<,>),
                typeof(IRequestExceptionHandler<,,>),
                typeof(IRequestExceptionAction<,>),
                typeof(INotificationHandler<>),
                typeof(IStreamRequestHandler<,>)
            };

        foreach (var mediatrOpenType in mediatrOpenTypes)
        {
            builder
                .RegisterAssemblyTypes(mediatorAssembly)
                .AsClosedTypesOf(mediatrOpenType)
                .AsImplementedInterfaces();
        }

        var services = new ServiceCollection();

        builder.Populate(services);
    }
}