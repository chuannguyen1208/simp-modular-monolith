using System.ComponentModel.DataAnnotations.Schema;

namespace Simp.Shared.Abstractions.Primitives;
public abstract class AggregateRoot(Guid id) : Entity(id)
{
    [NotMapped]
    public List<IDomainEvent> DomainEvents { get; init; } = [];

    public void ClearDomainEvents()
    {
        DomainEvents.Clear();
    }

    protected void RaiseDomainEvent(IDomainEvent domainEvent)
    {
        DomainEvents.Add(domainEvent);
    }
}
