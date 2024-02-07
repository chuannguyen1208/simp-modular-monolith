using Autofac;
using Microsoft.EntityFrameworkCore;
using Simp.Modules.Cshops.Infrastructure.EF;

namespace Simp.Modules.Cshops.Infrastructure.AutofacModules;
public class CshopDbContextModule(string connectionString) : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        var optionsBuilder = new DbContextOptionsBuilder<CshopDbContext>()
            .UseSqlServer(connectionString);

        builder.RegisterInstance(optionsBuilder.Options).SingleInstance();
        builder.RegisterType<CshopDbContext>().InstancePerLifetimeScope();
    }
}