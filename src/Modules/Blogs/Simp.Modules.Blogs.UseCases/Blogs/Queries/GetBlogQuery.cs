using AutoMapper;
using MediatR;
using Simp.Modules.Blogs.Contracts.Blogs;
using Simp.Modules.Blogs.Domain.Blogs;
using Simp.Shared.Abstractions.Repositories;

namespace Simp.Modules.Blogs.UseCases.Blogs.Queries;
public record GetBlogQuery(Guid Id) : IRequest<BlogResponse?>
{
    private class Handler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetBlogQuery, BlogResponse?>
    {
        public async Task<BlogResponse?> Handle(GetBlogQuery request, CancellationToken cancellationToken)
        {
            var repo = unitOfWork.GetRepository<Blog>();

            var entity = await repo.GetByIdAsync(request.Id);

            var response = mapper.Map<BlogResponse>(entity);

            return response;
        }
    }
}
