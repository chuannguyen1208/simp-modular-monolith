using Simp.Shared.Abstractions.Services;

namespace Simp.Modules.Blogs.Api.Services;
internal class MessageService : IMessage
{
    public string SayHello()
    {
        return "Blogs";
    }
}
