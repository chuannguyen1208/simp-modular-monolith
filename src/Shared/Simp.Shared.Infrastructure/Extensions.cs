using Autofac;
using Autofac.Extensions.DependencyInjection;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Serilog;
using Simp.Shared.Abstractions.Primitives;
using Simp.Shared.Infrastructure.Routing;
using System.Reflection;

namespace Simp.Shared.Infrastructure;

internal static class Extensions
{
    public static void AddInfrastructure(this WebApplicationBuilder builder)
    {
        builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
        builder.Host.UseSerilog();

        builder.Host.ConfigureContainer<ContainerBuilder>((_, cb) =>
        {
            cb.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly).AsImplementedInterfaces();
        });
    }

    public static void UseInfrastructure(this WebApplication app)
    {
        app.UseSerilogRequestLogging();

        app.UseExceptionHandler(configure =>
        {
            configure.Run(async context =>
            {
                var exceptionFeature = context.Features.GetRequiredFeature<IExceptionHandlerFeature>();

                var ex = exceptionFeature.Error;

                var error = ex switch
                {
                    ValidationException vex => new Error(400, vex.Message),
                    _ => new Error(500, "Internal Server Error")
                };

                await Results.Problem(statusCode: error.Status, detail: error.Message).ExecuteAsync(context);
            });
        });

        app.UseEndpoints();
    }
}
