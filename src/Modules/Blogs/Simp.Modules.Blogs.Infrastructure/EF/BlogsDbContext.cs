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
        PublishDomainEvents(CancellationToken.None).RunSynchronously();
        return res;
    }

    private async Task PublishDomainEvents(CancellationToken cancellationToken)
    {
        var tasks = new List<Task>();

        var aggregateRoots = ChangeTracker.Entries<AggregateRoot>()
            .Where(e => e.Entity.DomainEvents.Count != 0)
            .Select(e => e.Entity);

        foreach (var aggregateRoot in aggregateRoots)
        {
            var events = aggregateRoot.DomainEvents.Select(s => mediator.Publish(s, cancellationToken));
            tasks.AddRange(events);
            aggregateRoot.ClearDomainEvents();
        }

        await Task.WhenAll(tasks);
    }
}
