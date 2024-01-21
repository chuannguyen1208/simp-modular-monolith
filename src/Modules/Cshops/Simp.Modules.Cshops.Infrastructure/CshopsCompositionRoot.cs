using Autofac;
using Simp.Modules.Cshops.Infrastructure.Services;
using Simp.Modules.Cshops.UseCases.Ingredients;
using Simp.Shared.Infrastructure.Compositions;

namespace Simp.Modules.Cshops.Infrastructure;
public class CshopsCompositionRoot : CompositionRoot
{
    public override IContainer ConfigureContainer()
    {
        var builder = new ContainerBuilder();

        builder.RegisterModule(new MediatorModule(typeof(GetIngredientsQuery).Assembly));
        builder.RegisterType<CshopsMessageService>().AsImplementedInterfaces().SingleInstance();

        return builder.Build();
    }
}
