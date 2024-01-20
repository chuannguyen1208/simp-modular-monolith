using MediatR;

namespace Simp.Modules.Blogs.UseCases.Blogs.Queries;
public class GetBlogsQuery : IRequest<string>
{
    private class Handler : IRequestHandler<GetBlogsQuery, string>
    {
        public async Task<string> Handle(GetBlogsQuery request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            return "Blogs";
        }
    }
}
