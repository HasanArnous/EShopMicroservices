using MediatR;

namespace Ordering.Core.Domain.Abstractions;

public interface IDomainEvent : INotification
{
    public Guid EventId => Guid.NewGuid();
    public DateTime OccurredAt => DateTime.Now;
    public string EventType => GetType().AssemblyQualifiedName;
}
