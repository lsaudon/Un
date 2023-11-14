using Microsoft.VisualStudio.TestTools.UnitTesting;
using NFluent;
using Un.Domain;

namespace Un.Infrastructure.Tests;

[TestClass]
public class EventPublisherTest
{
  [TestMethod]
  public void GivenHandlerWhenPublishThenCallHandler()
  {
    EventHandler<EventA> handler = new();
    EventPublisher publisher = new();
    publisher.Subscribe(handler);

    publisher.Publish(new EventA());

    Check.That(handler.IsCalled).IsTrue();
  }

  [TestMethod]
  public void GivenDifferentHandlersWhenPublishThenCallRightHandler()
  {
    EventHandler<EventB> handlerB = new();
    EventHandler<EventA> handlerA = new();
    EventPublisher publisher = new();
    publisher.Subscribe(handlerA);
    publisher.Subscribe(handlerB);

    publisher.Publish(new EventB());

    Check.That(handlerA.IsCalled).IsFalse();
    Check.That(handlerB.IsCalled).IsTrue();
  }

  private sealed class EventHandler<TEvent> : IEventHandler<TEvent> where TEvent : IDomainEvent
  {
    public bool IsCalled { get; private set; }

    public void Handle(TEvent evt) => IsCalled = true;
  }

  private sealed class EventA : IDomainEvent
  {
    public object GetAggregateId() => "A";
  }

  private sealed class EventB : IDomainEvent
  {
    public object GetAggregateId() => "B";
  }
}