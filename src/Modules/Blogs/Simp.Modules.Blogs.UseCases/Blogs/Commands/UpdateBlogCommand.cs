﻿using FluentValidation;
using MediatR;
using Simp.Modules.Blogs.Domain.Blogs;
using Simp.Shared.Abstractions.Repositories;

namespace Simp.Modules.Blogs.UseCases.Blogs.Commands;

public record UpdateBlogCommand(Guid Id, string Title, string Description, string Content) : IRequest
{
    public class UpdateBlogCommandValidator : AbstractValidator<UpdateBlogCommand>
    {
        public UpdateBlogCommandValidator()
        {
            RuleFor(s => s.Title)
                .NotNull()
                .NotEmpty();

            RuleFor(s => s.Description)
                .NotEmpty()
                .NotNull();

            RuleFor(s => s.Content)
                .NotEmpty()
                .NotNull();
        }
    }

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
