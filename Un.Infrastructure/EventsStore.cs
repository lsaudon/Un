using Un.Domain;

namespace Un.Infrastructure;

public class EventsStore
{
  private readonly List<IDomainEvent> _events = [];

  public void Store(IDomainEvent evt) => _events.Add(evt);

  public IEnumerable<IDomainEvent> GetEventsOfAggregate<TAggregateId>(TAggregateId id) => _events.Where(o => o.GetAggregateId().Equals(id));
}
