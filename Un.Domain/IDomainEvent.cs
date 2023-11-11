namespace Un.Domain;

public interface IDomainEvent
{
    object GetAggregateId();
}