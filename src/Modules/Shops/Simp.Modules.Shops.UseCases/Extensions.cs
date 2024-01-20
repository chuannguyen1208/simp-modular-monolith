using MediatR.Pipeline;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Simp.Modules.Shops.UseCases.Ingredients.Queries;
using Autofac;

namespace Simp.Modules.Shops.UseCases;
public static class Extensions
{
    public static void AddUseCases(this WebApplicationBuilder builder)
    {
        builder.Host.ConfigureContainer<ContainerBuilder>((_, cb) =>
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
                cb
                    .RegisterAssemblyTypes(typeof(GetIngredientQuery).Assembly)
                    .AsClosedTypesOf(mediatrOpenType)
                    .AsImplementedInterfaces();
            }
        });
    }
}
