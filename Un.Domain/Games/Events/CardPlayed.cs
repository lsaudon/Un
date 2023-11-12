namespace Un.Domain.Games.Events;

public record CardPlayed(GameId Id, PlayerId PlayerId, Card Card) : IDomainEvent
{
    public object GetAggregateId() => Id;
}