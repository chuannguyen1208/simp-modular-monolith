using Microsoft.AspNetCore.Routing;

namespace Simp.Shared.Abstractions.Routing;
public interface IEndpointsDefinition
{
    public static abstract void ConfigureEndpoints(IEndpointRouteBuilder app);
}
