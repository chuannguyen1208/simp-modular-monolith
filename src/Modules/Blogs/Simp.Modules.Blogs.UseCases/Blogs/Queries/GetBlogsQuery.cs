using MediatR;
using Simp.Modules.Blogs.Domain.Entities;
using Simp.Shared.Abstractions.Repositories;

namespace Simp.Modules.Blogs.UseCases.Blogs.Queries;
public class GetBlogsQuery : IRequest<IEnumerable<Blog>>
{
    private class Handler(IUnitOfWork unitOfWork) : IRequestHandler<GetBlogsQuery, IEnumerable<Blog>>
    {
        public async Task<IEnumerable<Blog>> Handle(GetBlogsQuery request, CancellationToken cancellationToken)
        {
            var repository = unitOfWork.GetRepository<Blog>();
            var entities = await repository.GetAllAsync();

            return entities;
        }
    }
}
