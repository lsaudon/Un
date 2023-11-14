using Un.Domain;

namespace Un.Infrastructure;

public class EventPublisherWithStorage(EventsStore store, IEventPublisher publisher) : IEventPublisher
{
  public void Publish<TEvent>(TEvent evt) where TEvent : IDomainEvent
  {
    store.Store(evt);
    publisher.Publish(evt);
  }
}