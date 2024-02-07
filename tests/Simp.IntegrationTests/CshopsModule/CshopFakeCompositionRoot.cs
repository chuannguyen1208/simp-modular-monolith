using Autofac;
using Simp.Modules.Cshops.Infrastructure;

namespace Simp.IntegrationTests.CshopsModule;

public class CshopsFakeCompositionRoot(IConfiguration configuration) : CshopsCompositionRoot(configuration)
{
    protected override ContainerBuilder ConfigureContainerBuilder()
    {
        var builder = base.ConfigureContainerBuilder();
        return builder;
    }
}