using MediatR;
using Simp.Modules.Blogs.Contracts.Blogs;
using Simp.Modules.Blogs.Domain.Blogs;
using Simp.Shared.Abstractions.Repositories;

namespace Simp.Modules.Blogs.UseCases.Blogs.Queries;
public record GetBlogQuery(Guid Id) : IRequest<BlogResponse>
{
    private class Handler(IUnitOfWork unitOfWork) : IRequestHandler<GetBlogQuery, BlogResponse>
    {
        public async Task<BlogResponse> Handle(GetBlogQuery request, CancellationToken cancellationToken)
        {
            var repo = unitOfWork.GetRepository<Blog>();

            var entity = await repo.GetByIdAsync(request.Id) ?? throw new KeyNotFoundException();

            var response = new BlogResponse(
                Id: entity.Id,
                Title: entity.Title,
                Description: entity.Description,
                Content: entity.Content,
                Published: entity.Published);

            return response;
        }
    }
}
