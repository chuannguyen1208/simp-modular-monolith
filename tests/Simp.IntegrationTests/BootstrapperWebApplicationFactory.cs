using Autofac;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Simp.IntegrationTests.Compositions;
using Simp.Modules.Cshops.Infrastructure;

namespace Simp.IntegrationTests;

public class BootstrapperWebApplicationFactory<TProgram>
    : WebApplicationFactory<TProgram> where TProgram : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureLogging(builder =>
        {
            builder
                .ClearProviders()
                .AddConsole();
        });

        builder.ConfigureTestContainer<ContainerBuilder>(builder =>
        {
            builder
            .RegisterType<CshopsFakeCompositionRoot>()
            .As<CshopsCompositionRoot>().SingleInstance();
        });

        builder.UseEnvironment("Development");
    }
}
