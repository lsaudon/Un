using Microsoft.VisualStudio.TestTools.UnitTesting;
using NFluent;
using Un.Domain;

namespace Un.Infrastructure.Tests;

[TestClass]
public class EventsStoreTest
{
    private static readonly AgregateAId AgregateId1 = new("A");
    private static readonly AgregateAId AgregateId2 = new("B");

    private readonly EventsStore _store = new();

    [TestMethod]
    public void WhenStoreEventOfAggregateThenCanGetThisEventOfAggregate()
    {
        _store.Store(new EventA(AgregateId1));

        Check.That(_store.GetEventsOfAggregate(AgregateId1)).HasSize(1);
    }

    [TestMethod]
    public void GivenEventsOfSeveralAggregatesWhenGetEventsOfAggregateThenReturnEventsOfOnlyThisAggregate()
    {
        _store.Store(new EventA(AgregateId1));
        _store.Store(new EventA(AgregateId2));
        _store.Store(new EventA(AgregateId1));

        var eventsOfAggregateA = _store.GetEventsOfAggregate(AgregateId1).ToArray();

        Check.That(eventsOfAggregateA).HasSize(2);
        Check.That(eventsOfAggregateA.Cast<EventA>().Select(o => o.Id).Distinct()).ContainsExactly(AgregateId1);
    }

    [TestMethod]
    public void GivenSeveralEventsWhenGetEventsOfAggregateThenReturnEventsAndPreserveOrder()
    {
        _store.Store(new EventA(AgregateId1, 1));
        _store.Store(new EventA(AgregateId1, 2));
        _store.Store(new EventA(AgregateId1, 3));

        var eventsOfAggregateA = _store.GetEventsOfAggregate(AgregateId1).ToArray();

        Check.That(eventsOfAggregateA.Cast<EventA>().Select(o => o.Value)).ContainsExactly(1, 2, 3);
    }

    private readonly record struct EventA(AgregateAId Id, int Value = 0) : IDomainEvent
    {
        public object GetAggregateId() => Id;
    }

    private readonly record struct AgregateAId(string Id)
    {
        public string Id { get; } = Id;
    }
}