using MediatR;
using Simp.Modules.Blogs.Domain.Blogs;
using Simp.Shared.Abstractions.Repositories;

namespace Simp.Modules.Blogs.UseCases.Blogs.Commands;
public record UpdateBlogCommand : IRequest
{
    public UpdateBlogCommand(string title, string description, string content)
    {
        Title = title;
        Description = description;
        Content = content;
    }

    public Guid Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public string Content { get; set; }

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
