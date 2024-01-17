using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Simp.Modules.Shops.Infrastructure.EF;
using Simp.Modules.Shops.UseCases.Ingredients.Queries;


namespace Simp.Modules.Shops.Api;

internal static class ShopsModule
{
    public static void AddShopsModule(this WebApplicationBuilder builder)
    {
        builder.Services.AddMediatR(c => c.RegisterServicesFromAssemblyContaining<GetInrgedientsQuery>());
        builder.Services.AddDbContext<CshopDbContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("cshop"));
        });
    }

    public static void UseShopsModule(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<CshopDbContext>();
        dbContext.Database.Migrate();
    }
}
