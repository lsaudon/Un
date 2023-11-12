using Microsoft.VisualStudio.TestTools.UnitTesting;
using NFluent;
using Un.Domain.Games;
using Un.Domain.Games.Events;

namespace Un.Domain.Tests.Games;

[TestClass]
public class GameTest
{
  [TestMethod]
  public void WhenStartGameThenRaiseGameStarted()
  {
    EventPublisherFake eventPublisher = new();

    Game.Start(eventPublisher);

    GameStarted evt = (GameStarted)eventPublisher.Events.First();
    Check.That(evt.Card).Is(new Card(CardColor.Blue, CardValue.Zero));
  }

  [TestMethod]
  public void GivenGameStartedWhenPlayCardThenRaiseCardPlayed()
  {
    EventPublisherFake eventPublisher = new();
    GameId gameId = GameId.Generate();
    Game game = new(new List<IDomainEvent>
                    {
                      new GameStarted(gameId, new Card(CardColor.Blue, CardValue.Zero))
                    });

    Card card = new(CardColor.Blue, CardValue.One);
    PlayerId playerId = PlayerId.Generate();
    game.PlayCard(eventPublisher, playerId, card);

    Check.That(eventPublisher.Events)
         .ContainsExactly(new CardPlayed(gameId, playerId, card));
  }

  [TestMethod]
  public void GivenGameStartedWhenPlayWrongCardThenDoNotRaiseCardPlayed()
  {
    EventPublisherFake eventPublisher = new();
    GameId gameId = GameId.Generate();
    Game game = new(
                    new List<IDomainEvent> { new GameStarted(gameId, new Card(CardColor.Blue, CardValue.Zero)) });

    Card card = new(CardColor.Red, CardValue.One);
    game.PlayCard(eventPublisher, PlayerId.Generate(), card);

    Check.That(eventPublisher.Events).IsEmpty();
  }

  [TestMethod]
  public void GivenCardPlayedWhenPlayCardThenRaiseCardPlayed()
  {
    EventPublisherFake eventPublisher = new();
    GameId gameId = GameId.Generate();
    Game game = new(new IDomainEvent[]
                    {
                      new GameStarted(gameId, new Card(CardColor.Blue, CardValue.Zero)),
                      new CardPlayed(gameId, PlayerId.Generate(), new Card(CardColor.Blue, CardValue.One))
                    });

    Card card = new(CardColor.Red, CardValue.One);
    PlayerId playerId = PlayerId.Generate();
    game.PlayCard(eventPublisher, playerId, card);

    Check.That(eventPublisher.Events)
         .ContainsExactly(new CardPlayed(gameId, playerId, card));
  }

  [TestMethod]
  public void GivenCardPlayedWhenPlayWrongCardThenDoNotRaiseCardPlayed()
  {
    EventPublisherFake eventPublisher = new();
    GameId gameId = GameId.Generate();
    Game game = new(new IDomainEvent[]
                    {
                      new GameStarted(gameId, new Card(CardColor.Blue, CardValue.Zero)),
                      new CardPlayed(gameId, PlayerId.Generate(), new Card(CardColor.Blue, CardValue.One))
                    });

    Card card = new(CardColor.Red, CardValue.Zero);
    game.PlayCard(eventPublisher, PlayerId.Generate(), card);

    Check.That(eventPublisher.Events).IsEmpty();
  }

  [TestMethod]
  public void GivenCardPlayedWhenSamePlayerPlayCardThenDoNotRaiseCardPlayed()
  {
    EventPublisherFake eventPublisher = new();
    GameId gameId = GameId.Generate();
    PlayerId playerId = PlayerId.Generate();
    Game game = new(new IDomainEvent[]
                    {
                      new GameStarted(gameId, new Card(CardColor.Blue, CardValue.Zero)),
                      new CardPlayed(gameId, playerId, new Card(CardColor.Blue, CardValue.One))
                    });

    Card card = new(CardColor.Red, CardValue.One);
    game.PlayCard(eventPublisher, playerId, card);

    Check.That(eventPublisher.Events).IsEmpty();
  }
}