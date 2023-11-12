namespace Un.Domain.Games;

public class Game
{
    public static void Start(IEventPublisher eventPublisher)
    {
        eventPublisher.Publish(new GameStarted(GameId.Generate(), new Card(CardColor.Blue, CardValue.Zero)));
    }
}