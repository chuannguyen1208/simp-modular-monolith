using Microsoft.AspNetCore.Builder;
using Simp.Shared.Abstractions.Routing;
using System.Reflection;

namespace Simp.Shared.Infrastructure.Routing;
internal static class EndpointsBootstrapper
{
    public static void UseEndpoints(this IApplicationBuilder app)
    {
        var assemblies = AppDomain.CurrentDomain
            .GetAssemblies()
            .Where(assembly => assembly.FullName!.StartsWith("Simp.Modules"));

        foreach (var assembly in assemblies)
        {
            if (assembly is null)
            {
                throw new InvalidOperationException("Passed Assembly is null");
            }

            var endpointTypes = GetEndpointDefinitionsFromAssembly(assembly);

            foreach (var endpointType in endpointTypes)
            {
                endpointType.GetMethod(nameof(IEndpointsDefinition.ConfigureEndpoints))!
                    .Invoke(null,
                    [
                        app
                    ]);
            }
        }
    }

    private static IEnumerable<TypeInfo> GetEndpointDefinitionsFromAssembly(Assembly assembly)
    {
        var endpointDefinitions = assembly.DefinedTypes
                .Where(x =>
                    x is { IsAbstract: false, IsInterface: false }
                    && typeof(IEndpointsDefinition).IsAssignableFrom(x)
                );

        return endpointDefinitions;
    }
}
