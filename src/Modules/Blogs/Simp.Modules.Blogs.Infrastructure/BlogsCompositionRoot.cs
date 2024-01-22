﻿using Autofac;
using Simp.Modules.Blogs.Infrastructure.AutofacModules;
using Simp.Modules.Blogs.UseCases.Blogs.Queries;
using Simp.Shared.Infrastructure.Compositions;

namespace Simp.Modules.Blogs.Infrastructure;
public class BlogsCompositionRoot : CompositionRoot
{
    public override IContainer ConfigureContainer()
    {
        var builder = new ContainerBuilder();

        builder.RegisterModule(new MediatorModule(typeof(GetBlogsQuery).Assembly));
        builder.RegisterModule<DbContextModule>();

        return builder.Build();
    }
}
