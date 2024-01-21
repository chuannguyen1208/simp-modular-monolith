using Simp.Shared.Infrastructure.Services;

namespace Simp.Modules.Cshops.Infrastructure.Services;
internal class CshopsMessageService : MessageService
{
    public override string SayHello()
    {
        return "Hello from cshops";
    }
}
