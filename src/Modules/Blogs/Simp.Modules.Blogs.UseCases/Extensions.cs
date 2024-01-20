using Autofac;
using MediatR;
using MediatR.Pipeline;
using Microsoft.AspNetCore.Builder;
using Simp.Modules.Blogs.UseCases.Blogs.Queries;

namespace Simp.Modules.Blogs.UseCases;
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
                    .RegisterAssemblyTypes(typeof(GetBlogsQuery).Assembly)
                    .AsClosedTypesOf(mediatrOpenType)
                    .AsImplementedInterfaces();
            }
        });
    }
}
