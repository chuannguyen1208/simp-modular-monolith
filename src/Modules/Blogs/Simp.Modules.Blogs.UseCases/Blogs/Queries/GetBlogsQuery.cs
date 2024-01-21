using MediatR;
using Simp.Shared.Abstractions.Services;

namespace Simp.Modules.Blogs.UseCases.Blogs.Queries;
public class GetBlogsQuery : IRequest<string>
{
    private class Handler(IMessageService messageService) : IRequestHandler<GetBlogsQuery, string>
    {
        public async Task<string> Handle(GetBlogsQuery request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(messageService.SayHello());
        }
    }
}
