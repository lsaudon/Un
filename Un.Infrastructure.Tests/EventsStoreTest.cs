using Microsoft.VisualStudio.TestTools.UnitTesting;
using NFluent;
using Un.Domain;

namespace Un.Infrastructure.Tests;

[TestClass]
public class EventsStoreTest
{
  private static readonly AggregateAId AggregateId1 = new("A");
  private static readonly AggregateAId AggregateId2 = new("B");

  private readonly EventsStore _store = new();

  [TestMethod]
  public void WhenStoreEventOfAggregateThenCanGetThisEventOfAggregate()
  {
    _store.Store(new EventA(AggregateId1));

    Check.That(_store.GetEventsOfAggregate(AggregateId1)).HasSize(1);
  }

  [TestMethod]
  public void GivenEventsOfSeveralAggregatesWhenGetEventsOfAggregateThenReturnEventsOfOnlyThisAggregate()
  {
    _store.Store(new EventA(AggregateId1));
    _store.Store(new EventA(AggregateId2));
    _store.Store(new EventA(AggregateId1));

    IDomainEvent[] eventsOfAggregateA = _store.GetEventsOfAggregate(AggregateId1).ToArray();

    Check.That(eventsOfAggregateA).HasSize(2);
    Check.That(eventsOfAggregateA.Cast<EventA>().Select(o => o.Id).Distinct()).ContainsExactly(AggregateId1);
  }

  [TestMethod]
  public void GivenSeveralEventsWhenGetEventsOfAggregateThenReturnEventsAndPreserveOrder()
  {
    _store.Store(new EventA(AggregateId1, 1));
    _store.Store(new EventA(AggregateId1, 2));
    _store.Store(new EventA(AggregateId1, 3));

    IDomainEvent[] eventsOfAggregateA = _store.GetEventsOfAggregate(AggregateId1).ToArray();

    Check.That(eventsOfAggregateA.Cast<EventA>().Select(o => o.Value)).ContainsExactly(1, 2, 3);
  }

  private readonly record struct EventA(AggregateAId Id, int Value = 0) : IDomainEvent
  {
    public object GetAggregateId() => Id;
  }

  private readonly record struct AggregateAId(string Id)
  {
    public string Id { get; } = Id;
  }
}