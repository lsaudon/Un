using Microsoft.VisualStudio.TestTools.UnitTesting;
using NFluent;
using Un.Domain;
using Un.Domain.Tests;

namespace Un.Infrastructure.Tests;

[TestClass]
public class EventPublisherWithStorageTest
{
  private readonly EventsStore _store;
  private readonly EventPublisherWithStorage _publisher;
  private readonly EventPublisherFake _publisherBase;

  public EventPublisherWithStorageTest()
  {
    _store = new EventsStore();
    _publisherBase = new EventPublisherFake();
    _publisher = new EventPublisherWithStorage(_store, _publisherBase);
  }

  [TestMethod]
  public void WhenPublishEventThenStoreInDatabase()
  {
    _publisher.Publish(new EventA());

    Check.That(_store.GetEventsOfAggregate("A")).HasSize(1);
  }

  [TestMethod]
  public void WhenPublishEventThenCallEventHandlerBase()
  {
    _publisher.Publish(new EventA());

    Check.That(_publisherBase.Events).HasSize(1);
  }

  private readonly record struct EventA : IDomainEvent
  {
    public object GetAggregateId() => "A";
  }
}