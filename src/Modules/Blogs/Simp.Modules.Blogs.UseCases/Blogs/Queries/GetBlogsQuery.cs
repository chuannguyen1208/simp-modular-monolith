using MediatR;
using Simp.Modules.Blogs.Contracts.Blogs;
using Simp.Modules.Blogs.Domain.Blogs;
using Simp.Shared.Abstractions.Repositories;

namespace Simp.Modules.Blogs.UseCases.Blogs.Queries;
public class GetBlogsQuery : IRequest<IEnumerable<BlogResponse>>
{
    private class Handler(IUnitOfWork unitOfWork) : IRequestHandler<GetBlogsQuery, IEnumerable<BlogResponse>>
    {
        public async Task<IEnumerable<BlogResponse>> Handle(GetBlogsQuery request, CancellationToken cancellationToken)
        {
            var repository = unitOfWork.GetRepository<Blog>();

            var entities = await repository.GetAllAsync();
            
            var res = entities.Select(s => new BlogResponse(s.Id, s.Title, s.Description, s.Content, s.Published));

            return res;
        }
    }
}
