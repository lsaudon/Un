using Un.Domain;

namespace Un.Infrastructure;

public class EventPublisher : IEventPublisher, IEventSubscriber
{
  private readonly List<IEventHandler> _handlers = [];

  public void Subscribe(IEventHandler handler) => _handlers.Add(handler);

  public void Publish<TEvent>(TEvent evt) where TEvent : IDomainEvent
  {
    foreach (IEventHandler<TEvent> handler in _handlers.OfType<IEventHandler<TEvent>>())
    {
      handler.Handle(evt);
    }
  }
}