using Simp.Shared.Abstractions.Primitives;

namespace Simp.Modules.Blogs.Domain.Blogs.DomainEvents;

public record BlogCreated(Guid Id) : IDomainEvent
{
}
