namespace Simp.Shared.Abstractions.Primitives;
public class Entity
{
    protected Entity(Guid id)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentException(nameof(Id));
        }

        Id = id;
    }

    public Guid Id { get; private set; }
}
