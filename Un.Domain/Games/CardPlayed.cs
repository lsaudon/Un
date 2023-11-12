namespace Un.Domain.Games;

public record CardPlayed(GameId Id, Card Card) : IDomainEvent
{
    public object GetAggregateId() => Id;
}