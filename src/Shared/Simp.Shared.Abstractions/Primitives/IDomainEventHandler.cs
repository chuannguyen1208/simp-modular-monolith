using MediatR;

namespace Simp.Shared.Abstractions.Primitives;
public interface IDomainEventHandler<TEvent> : INotificationHandler<TEvent> where TEvent : IDomainEvent { }
