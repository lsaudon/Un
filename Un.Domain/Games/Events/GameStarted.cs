namespace Un.Domain.Games.Events;

public record GameStarted(GameId Id, Card Card) : IDomainEvent
{
    public object GetAggregateId() => Id;
}