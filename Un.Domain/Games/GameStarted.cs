namespace Un.Domain.Games;

public record GameStarted(GameId Id, Card Card) : IDomainEvent
{
    public object GetAggregateId() => Id;
}