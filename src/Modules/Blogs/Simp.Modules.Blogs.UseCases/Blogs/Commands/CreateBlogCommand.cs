﻿using FluentValidation;
using MediatR;
using Simp.Modules.Blogs.Contracts.Blogs;
using Simp.Modules.Blogs.Domain.Blogs;
using Simp.Modules.Blogs.Domain.Contents;
using Simp.Shared.Abstractions.Repositories;

namespace Simp.Modules.Blogs.UseCases.Blogs.Commands;
public record CreateBlogCommand : BlogRequest, IRequest<Guid>
{
    public CreateBlogCommand(string Title, string Description, string Content) : base(Title, Description, Content)
    {
    }

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

            var blog = Blog.Create(request.Title, request.Description, request.Content, contentHtml, false);

            await repo.CreateAsync(blog);
            await unitOfWork.SaveChangesAsync();

            return blog.Id;
        }
    }
}
