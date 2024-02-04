using MediatR;
using Microsoft.EntityFrameworkCore;
using Simp.Modules.Blogs.Domain.Blogs;
using Simp.Shared.Abstractions.Repositories;

namespace Simp.Modules.Blogs.UseCases.Blogs.Queries;
public record GetBlogsSumaryQuery : IRequest<BlogsSummaryResponse>
{
    private class Handler(IUnitOfWork unitOfWork) : IRequestHandler<GetBlogsSumaryQuery, BlogsSummaryResponse>
    {
        public async Task<BlogsSummaryResponse> Handle(GetBlogsSumaryQuery request, CancellationToken cancellationToken)
        {
            var repo = unitOfWork.GetRepository<Blog>();

            var blogsCount = await repo.Entities.CountAsync(cancellationToken: cancellationToken);

            var res = new BlogsSummaryResponse(blogsCount, 0, 0);

            return res;
        }
    }
}
