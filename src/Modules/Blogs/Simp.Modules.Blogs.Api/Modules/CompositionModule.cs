using Autofac;
using Autofac.Extensions.DependencyInjection;
using MediatR.Pipeline;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Simp.Modules.Blogs.UseCases.Blogs.Queries;
using System.Reflection;
using Simp.Modules.Blogs.Infrastructure;

namespace Simp.Modules.Blogs.Api.Modules;

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
                .RegisterAssemblyTypes(typeof(GetBlogsQuery).Assembly)
                .AsClosedTypesOf(mediatrOpenType)
                .AsImplementedInterfaces();
        }

        var services = new ServiceCollection();

        builder.Populate(services);

        var container = builder.Build();
        return container;
    }
}