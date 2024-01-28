using MediatR;
using Simp.Modules.Blogs.Domain.Blogs;
using Simp.Shared.Abstractions.Repositories;

namespace Simp.Modules.Blogs.UseCases.Blogs.Commands;
internal record UpdateBlogCommand(Guid Id, string Title, string Description, string Content) : IRequest
{
    private class Handler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateBlogCommand>
    {
        public async Task Handle(UpdateBlogCommand request, CancellationToken cancellationToken)
        {
            var repo = unitOfWork.GetRepository<Blog>();
            
            var entity = await repo.GetByIdAsync(request.Id).ConfigureAwait(false);

            if (entity is null)
            {
                return;
            }

            entity.Update(request.Title, request.Description, request.Content);

            await unitOfWork.SaveChangesAsync();
        }
    }
}
