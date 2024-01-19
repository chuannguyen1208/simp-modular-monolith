using Autofac;
using MediatR.Pipeline;
using MediatR;
using Simp.Modules.Shops.UseCases.Ingredients.Queries;

namespace Simp.Modules.Shops.Api;
internal class ShopsModuleAutofac : Module
{
    protected override void Load(ContainerBuilder builder)
    {
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
                .RegisterAssemblyTypes(typeof(GetInrgedientsQuery).Assembly)
                .AsClosedTypesOf(mediatrOpenType)
                .AsImplementedInterfaces();
        }
    }
}
