namespace Un.Domain.Games.Events;

public readonly record struct GameStarted(GameId Id, Card Card) : IDomainEvent
{
    public object GetAggregateId() => Id;
}