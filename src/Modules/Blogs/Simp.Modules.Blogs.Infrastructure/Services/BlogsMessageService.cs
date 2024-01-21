using Simp.Shared.Infrastructure.Services;

namespace Simp.Modules.Blogs.Infrastructure.Services;
internal class BlogsMessageService : MessageService
{
    public override string SayHello()
    {
        return "Hello from blogs";
    }
}
