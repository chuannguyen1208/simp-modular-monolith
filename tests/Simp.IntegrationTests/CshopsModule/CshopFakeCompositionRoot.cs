using Autofac;
using Microsoft.EntityFrameworkCore;
using Simp.Modules.Cshops.Infrastructure;
using Simp.Modules.Cshops.Infrastructure.EF;

namespace Simp.IntegrationTests.CshopsModule;

public class CshopsFakeCompositionRoot(IConfiguration configuration) : CshopsCompositionRoot(configuration)
{
    protected override ContainerBuilder ConfigureContainerBuilder()
    {
        var builder = base.ConfigureContainerBuilder();
        
        var dbContextOptions = new DbContextOptionsBuilder<CshopDbContext>()
            .UseInMemoryDatabase("cshop-integration")
            .Options;
        builder.RegisterInstance(dbContextOptions).SingleInstance();

        return builder;
    }
}