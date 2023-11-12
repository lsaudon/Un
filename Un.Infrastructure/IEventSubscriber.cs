using Un.Domain;

namespace Un.Infrastructure;

public interface IEventSubscriber
{
    void Subscribe(IEventHandler handler);
}