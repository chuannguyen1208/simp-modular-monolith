using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Simp.Modules.Shops.Api;

internal static class EndpointExtensions
{
    public static RouteHandlerBuilder WithIngredientTags(this RouteHandlerBuilder builder) 
        => builder.WithTags("Shops.Ingredients");
}
