using MediatR;
using Simp.Modules.Blogs.Domain.Blogs;
using Simp.Shared.Abstractions.Repositories;

namespace Simp.Modules.Blogs.UseCases.Blogs.Commands;
public record DeleteBlogCommand(Guid Id) : IRequest
{
    private class Handler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteBlogCommand>
    {
        public async Task Handle(DeleteBlogCommand request, CancellationToken cancellationToken)
        {
            var repo = unitOfWork.GetRepository<Blog>();

            await repo.DeleteAsync(request.Id).ConfigureAwait(false);

            await unitOfWork.SaveChangesAsync();
        }
    }
}
