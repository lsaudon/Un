namespace Un.Domain.Games;

public record GameStarted(GameId Id, Card LastCard) : IDomainEvent
{
    public object GetAggregateId() => Id;
}