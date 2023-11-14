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

  public static GameId Start(IEventPublisher eventPublisher)
  {
    GameId id = GameId.Generate();
    eventPublisher.Publish(new GameStarted(id, new Card(CardColor.Blue, CardValue.Zero)));
    return id;
  }

  public void PlayCard(IEventPublisher eventPublisher, PlayerId playerId, Card card)
  {
    if (IsNotPlayable(_projection.LastPlayer, playerId, _projection.TopCard, card))
    {
      return;
    }

    eventPublisher.Publish(new CardPlayed(_projection.Id, playerId, card));
  }

  private static bool IsNotPlayable(PlayerId playerA, PlayerId playerB, Card cardA, Card cardB) =>
    IsNotPlayablePlayer(playerA, playerB) || IsNotPlayableCard(cardA, cardB);

  private static bool IsNotPlayablePlayer(PlayerId a, PlayerId b) => a == b;

  private static bool IsNotPlayableCard(Card a, Card b) => a.Color != b.Color && a.Value != b.Value;

  private sealed class DecisionProjection : DecisionProjectionBase
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