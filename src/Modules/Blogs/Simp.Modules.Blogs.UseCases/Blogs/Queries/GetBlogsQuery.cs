using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Simp.Modules.Blogs.Contracts.Blogs;
using Simp.Modules.Blogs.Domain.Blogs;
using Simp.Shared.Abstractions.Repositories;

namespace Simp.Modules.Blogs.UseCases.Blogs.Queries;
public class GetBlogsQuery : IRequest<IEnumerable<BlogResponse>>
{
    private class Handler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetBlogsQuery, IEnumerable<BlogResponse>>
    {
        public async Task<IEnumerable<BlogResponse>> Handle(GetBlogsQuery request, CancellationToken cancellationToken)
        {
            var repository = unitOfWork.GetRepository<Blog>();

            var queryable = repository.Entities;

            var res = await mapper.ProjectTo<BlogResponse>(queryable).ToListAsync(cancellationToken: cancellationToken);
            
            return res;
        }
    }
}
