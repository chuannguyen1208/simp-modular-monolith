using FluentValidation;
using MediatR;
using Simp.Modules.Blogs.Contracts.Blogs;
using Simp.Modules.Blogs.Domain.Blogs;
using Simp.Modules.Blogs.Domain.Contents;
using Simp.Shared.Abstractions.Repositories;

namespace Simp.Modules.Blogs.UseCases.Blogs.Commands;
public record CreateBlogCommand(string Title, string Description, string Content, bool Published, bool IsTemplate) :
    BlogRequest(Title, Description, Content, Published, IsTemplate),
    IRequest<Guid>
{
    public class CreateBlogCommandValidator : AbstractValidator<CreateBlogCommand>
    {
        public CreateBlogCommandValidator()
        {
            RuleFor(s => s.Title)
                .NotNull()
                .NotEmpty();

            RuleFor(s => s.Description)
                .NotNull()
                .NotEmpty();

            RuleFor(s => s.Content)
                .NotNull()
                .NotEmpty();
        }
    }

    private class Handler(IUnitOfWork unitOfWork, IContentProcessor contentProcessor) : IRequestHandler<CreateBlogCommand, Guid>
    {
        public async Task<Guid> Handle(CreateBlogCommand request, CancellationToken cancellationToken)
        {
            var repo = unitOfWork.GetRepository<Blog>();

            var contentHtml = await contentProcessor.ProcessContent(request.Content);

            var blog = Blog.Create(request.Title, request.Description, request.Content, contentHtml);

            blog.UpdatePublished(request.Published);
            blog.UpdateIsTemplate(blog.IsTemplate);

            await repo.CreateAsync(blog);
            await unitOfWork.SaveChangesAsync();

            return blog.Id;
        }
    }
}
