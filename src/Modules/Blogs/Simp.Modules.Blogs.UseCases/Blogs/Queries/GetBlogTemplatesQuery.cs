using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Simp.Modules.Blogs.Contracts.Blogs;
using Simp.Modules.Blogs.Domain.Blogs;
using Simp.Shared.Abstractions.Repositories;

namespace Simp.Modules.Blogs.UseCases.Blogs.Queries;
public record GetBlogTemplatesQuery : IRequest<IEnumerable<BlogResponse>>
{
    private class Handler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetBlogTemplatesQuery, IEnumerable<BlogResponse>>
    {
        public async Task<IEnumerable<BlogResponse>> Handle(GetBlogTemplatesQuery request, CancellationToken cancellationToken)
        {
            var repo = unitOfWork.GetRepository<Blog>();

            var queryably = repo.Entities.Where(s => s.IsTemplate);

            var res = await mapper.ProjectTo<BlogResponse>(queryably).ToListAsync(cancellationToken: cancellationToken);

            return res;
        }
    }
}
