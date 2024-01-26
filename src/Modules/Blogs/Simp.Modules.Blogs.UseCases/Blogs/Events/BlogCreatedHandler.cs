using Simp.Modules.Blogs.Domain.Blogs;
using Simp.Modules.Blogs.Domain.Blogs.DomainEvents;
using Simp.Shared.Abstractions.Primitives;
using Simp.Shared.Abstractions.Repositories;

namespace Simp.Modules.Blogs.UseCases.Blogs.Events;
internal class BlogCreatedHandler(IUnitOfWork unitOfWork) : IDomainEventHandler<BlogCreated>
{
    public async Task Handle(BlogCreated notification, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.GetRepository<Blog>();
        var blog = await repository.GetByIdAsync(notification.Id);
        
        if (blog is null)
        {
            return;
        }

        blog.UpdatePublished(true);
        await repository.UpdateAsync(blog);
        await unitOfWork.SaveChangesAsync();
    }
}
