using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Simp.Modules.Blogs.Domain.Blogs;
using Simp.Shared.Abstractions.Primitives;

namespace Simp.Modules.Blogs.Infrastructure.EF;

public class BlogsDbContext(DbContextOptions<BlogsDbContext> options, IMediator mediator) : DbContext(options)
{
    public DbSet<Blog> Blogs { get; set; }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var res = await base.SaveChangesAsync(cancellationToken);
        await PublishDomainEvents(cancellationToken);
        return res;
    }

    public override int SaveChanges()
    {
        var res = base.SaveChanges();
        PublishDomainEvents(CancellationToken.None).GetAwaiter().GetResult();
        return res;
    }

    private async Task PublishDomainEvents(CancellationToken cancellationToken)
    {
        List<EntityEntry<AggregateRoot>> aggregateRoots = ChangeTracker
                .Entries<AggregateRoot>()
                .Where(entityEntry => entityEntry.Entity.DomainEvents.Count != 0)
                .ToList();

        List<IDomainEvent> domainEvents = aggregateRoots.SelectMany(entityEntry => entityEntry.Entity.DomainEvents).ToList();

        aggregateRoots.ForEach(entityEntry => entityEntry.Entity.ClearDomainEvents());

        IEnumerable<Task> tasks = domainEvents.Select(domainEvent => mediator.Publish(domainEvent, cancellationToken));

        await Task.WhenAll(tasks);
    }
}
