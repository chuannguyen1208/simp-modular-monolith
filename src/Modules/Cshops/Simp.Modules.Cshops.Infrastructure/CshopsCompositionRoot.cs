using Autofac;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Serilog;
using Simp.Modules.Cshops.Infrastructure.AutofacModules;
using Simp.Modules.Cshops.Infrastructure.EF;
using Simp.Modules.Cshops.Infrastructure.Services;
using Simp.Modules.Cshops.UseCases.Ingredients.Queries;
using Simp.Shared.Abstractions.Compositions;
using Simp.Shared.Infrastructure.Compositions;
using Simp.Shared.Infrastructure.Repositories;

namespace Simp.Modules.Cshops.Infrastructure;

public interface ICshopsCompositionRoot : ICompositionRoot;

public class CshopsCompositionRoot(IConfiguration configuration) : CompositionRoot, ICshopsCompositionRoot
{
    protected override ContainerBuilder ConfigureContainerBuilder()
    {
        var builder = new ContainerBuilder();

        var connectionString = configuration.GetConnectionString("cshop") ?? "";

        builder.RegisterModule(new CshopDbContextModule(connectionString));
        builder.RegisterModule(new MediatorModule(typeof(GetIngredientsQuery).Assembly));
        builder.RegisterModule(new ServicesModule());

        return builder;
    }

}
