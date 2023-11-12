namespace Un.Domain.Games.Events;

public readonly record struct CardPlayed(GameId Id, PlayerId PlayerId, Card Card) : IDomainEvent
{
  public object GetAggregateId() => Id;
}