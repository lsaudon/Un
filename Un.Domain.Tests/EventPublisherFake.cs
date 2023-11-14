namespace Un.Domain.Tests;

public class EventPublisherFake : IEventPublisher
{
  private readonly List<IDomainEvent> _events = [];

  public IEnumerable<IDomainEvent> Events => _events;

  public void Publish<TEvent>(TEvent evt) where TEvent : IDomainEvent => _events.Add(evt);
}