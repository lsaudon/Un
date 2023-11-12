namespace Un.Domain.Games;

public class Game
{
    private readonly DecisionProjection _projection = new();

    public Game(IEnumerable<IDomainEvent> events)
    {
        foreach (var evt in events)
        {
            _projection.Apply(evt);
        }
    }

    public static void Start(IEventPublisher eventPublisher)
    {
        eventPublisher.Publish(new GameStarted(GameId.Generate(), new Card(CardColor.Blue, CardValue.Zero)));
    }

    public void PlayCard(IEventPublisher eventPublisher, Card card, PlayerId playerId)
    {
        if (_projection.LastPlayer == playerId ||
            _projection.LastCard.Color != card.Color && _projection.LastCard.Value != card.Value) return;

        eventPublisher.Publish(new CardPlayed(_projection.Id, playerId, card));
    }

    private class DecisionProjection : DecisionProjectionBase
    {
        public GameId Id { get; private set; }
        public PlayerId LastPlayer { get; private set; }
        public Card LastCard { get; private set; }

        public DecisionProjection()
        {
            AddHandler<GameStarted>(When);
            AddHandler<CardPlayed>(When);
        }

        private void When(GameStarted evt)
        {
            Id = evt.Id;
            LastCard = evt.Card;
        }

        private void When(CardPlayed evt)
        {
            LastPlayer = evt.PlayerId;
            LastCard = evt.Card;
        }
    }
}