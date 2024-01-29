using Autofac;
using AutoMapper;
using Simp.Modules.Blogs.UseCases.Blogs;

namespace Simp.Modules.Blogs.Infrastructure.AutofacModules;

internal class AutoMapperModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<BlogProfile>();
        });

        var mapper = new Mapper(config);

        builder.RegisterInstance(mapper)
            .AsImplementedInterfaces()
            .SingleInstance();
    }
}