using Autofac;
using MediatR.Pipeline;
using MediatR;
using Microsoft.AspNetCore.Builder;
using System.Reflection;
using Simp.Modules.Blogs.UseCases.Blogs.Queries;
using Microsoft.Extensions.DependencyInjection;
using Autofac.Extensions.DependencyInjection;
using Simp.Modules.Blogs.Api.Modules;

namespace Simp.Modules.Blogs.Api;

public static class Extensions
{
    public static void AddBlogsModule(this WebApplicationBuilder builder)
    {
        builder.Host.ConfigureContainer<ContainerBuilder>((_, cb) =>
        {
            cb.RegisterModule<CompositionModule>();
            cb.RegisterModule<MediatorModule>();
        });
    }
}
