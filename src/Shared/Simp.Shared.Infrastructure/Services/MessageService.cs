using Simp.Shared.Abstractions.Services;

namespace Simp.Shared.Infrastructure.Services;
public abstract class MessageService : IMessageService
{
    public virtual string SayHello()
    {
        return "Hello from shared";
    }
}
