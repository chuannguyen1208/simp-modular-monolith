using Autofac;
using Microsoft.EntityFrameworkCore;
using Simp.Modules.Blogs.Infrastructure.EF;

namespace Simp.Modules.Blogs.Infrastructure.AutofacModules;
public class DbContextModule(string connectionString) : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        var optionsBuilder = new DbContextOptionsBuilder<BlogsDbContext>()
            .UseSqlServer(connectionString);

        builder.RegisterInstance(optionsBuilder.Options).SingleInstance();
        builder.RegisterType<BlogsDbContext>().InstancePerLifetimeScope();
    }
}
