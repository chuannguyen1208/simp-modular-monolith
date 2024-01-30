using Autofac.Extensions.DependencyInjection;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Simp.Shared.Abstractions.Primitives;
using Simp.Shared.Infrastructure.Routing;
using System.Text.Json;

namespace Simp.Shared.Infrastructure;

internal static class Extensions
{
    public static void AddInfrastructure(this WebApplicationBuilder builder)
    {
        builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
    }

    public static void UseInfrastructure(this WebApplication app)
    {
        app.UseExceptionHandler(configure =>
        {
            configure.Run(async context =>
            {
                var exceptionFeature = context.Features.GetRequiredFeature<IExceptionHandlerFeature>();

                var ex = exceptionFeature.Error;

                int statusCode = StatusCodes.Status500InternalServerError;
                var errors = new List<Error>();

                if (ex is ValidationException vex)
                {
                    statusCode = StatusCodes.Status400BadRequest;
                    errors.AddRange(vex.Errors.Select(e => new Error(e.ErrorCode, e.ErrorMessage)));
                }
                else
                {
                    errors.Add(new Error("InternalServerError", "Internal Server Error"));
                }

                await Results.Problem(statusCode: statusCode, detail: JsonSerializer.Serialize(errors)).ExecuteAsync(context);
            });
        });

        app.UseEndpoints();
    }
}
