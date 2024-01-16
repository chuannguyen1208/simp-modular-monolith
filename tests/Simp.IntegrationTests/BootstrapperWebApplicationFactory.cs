using Microsoft.AspNetCore.Mvc.Testing;

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

        builder.UseEnvironment("Development");
    }
}
