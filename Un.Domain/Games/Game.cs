using Un.Domain.Games.Events;

namespace Un.Domain.Games;

public class Game
{
  private readonly DecisionProjection _projection = new();

  public Game(IEnumerable<IDomainEvent> events)
  {
    foreach (IDomainEvent evt in events)
    {
      _projection.Apply(evt);
    }
  }

  public static void Start(IEventPublisher eventPublisher)
  {
    eventPublisher.Publish(new GameStarted(GameId.Generate(), new Card(CardColor.Blue, CardValue.Zero)));
  }

  public void PlayCard(IEventPublisher eventPublisher, PlayerId playerId, Card card)
  {
    if (_projection.LastPlayer == playerId ||
        _projection.TopCard.Color != card.Color && _projection.TopCard.Value != card.Value)
    {
      return;
    }

    eventPublisher.Publish(new CardPlayed(_projection.Id, playerId, card));
  }

  private class DecisionProjection : DecisionProjectionBase
  {
    public GameId Id { get; private set; } = new(string.Empty);
    public PlayerId LastPlayer { get; private set; } = new(string.Empty);
    public Card TopCard { get; private set; }

    public DecisionProjection()
    {
      AddHandler<GameStarted>(When);
      AddHandler<CardPlayed>(When);
    }

    private void When(GameStarted evt)
    {
      Id = evt.Id;
      TopCard = evt.Card;
    }

    private void When(CardPlayed evt)
    {
      LastPlayer = evt.PlayerId;
      TopCard = evt.Card;
    }
  }
}