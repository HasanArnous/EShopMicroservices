namespace Ordering.Core.Domain.Abstractions;

public abstract class Aggregate<T> : Entity<T>, IAggregate<T>
{
    private List<IDomainEvent> _domainEvents = new();
    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents;
    
    public void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public IDomainEvent[] ClearEvents()
    {
        var removedEvents = _domainEvents.ToArray();
        _domainEvents.Clear();
        return removedEvents;
    }
}
