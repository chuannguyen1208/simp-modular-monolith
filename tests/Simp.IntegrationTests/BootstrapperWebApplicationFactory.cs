using Autofac;
using Microsoft.AspNetCore.Mvc.Testing;
using Simp.IntegrationTests.BlogsModule;
using Simp.IntegrationTests.CshopsModule;
using Simp.Modules.Blogs.Infrastructure;
using Simp.Modules.Cshops.Infrastructure;

namespace Simp.IntegrationTests;

public class BootstrapperWebApplicationFactory<TProgram>
    : WebApplicationFactory<TProgram> where TProgram : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Integration");
    }

    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.ConfigureContainer<ContainerBuilder>(cb =>
        {
            cb.RegisterType<BlogFakeCompositionRoot>().As<IBlogsCompositionRoot>().SingleInstance();
            cb.RegisterType<CshopsFakeCompositionRoot>().As<ICshopsCompositionRoot>().SingleInstance();
        });

        return base.CreateHost(builder);
    }
}
