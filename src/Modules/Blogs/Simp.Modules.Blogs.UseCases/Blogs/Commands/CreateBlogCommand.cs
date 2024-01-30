using FluentValidation;
using MediatR;
using Simp.Modules.Blogs.Domain.Blogs;
using Simp.Shared.Abstractions.Repositories;

namespace Simp.Modules.Blogs.UseCases.Blogs.Commands;
public record CreateBlogCommand(string Title, string Description, string Content) : IRequest<Guid>
{
    public class CreateBlogCommandValidator : AbstractValidator<CreateBlogCommand>
    {
        public CreateBlogCommandValidator()
        {
            RuleFor(s => s.Title).NotNull().NotEmpty().WithMessage("Title is required");
            RuleFor(s => s.Description).NotNull().NotEmpty().WithMessage("Description is required");
            RuleFor(s => s.Content).NotNull().NotEmpty().WithMessage("Content is required");
        }
    }

    private class Handler(IUnitOfWork unitOfWork) : IRequestHandler<CreateBlogCommand, Guid>
    {
        public async Task<Guid> Handle(CreateBlogCommand request, CancellationToken cancellationToken)
        {
            var repo = unitOfWork.GetRepository<Blog>();
            var blog = Blog.Create(request.Title, request.Description, request.Content, false);

            await repo.CreateAsync(blog);
            await unitOfWork.SaveChangesAsync();

            return blog.Id;
        }
    }
}
