using Microsoft.VisualStudio.TestTools.UnitTesting;
using NFluent;
using Un.Domain.Games;

namespace Un.Domain.Tests.Games;

[TestClass]
public class GameTest
{
    [TestMethod]
    public void WhenStartGameThenRaiseGameStarted()
    {
        EventPublisherFake eventPublisher = new();

        Game.Start(eventPublisher);

        var evt = (GameStarted)eventPublisher.Events.First();
        Check.That(evt.Card).Is(new Card(CardColor.Blue, CardValue.Zero));
    }

    [TestMethod]
    public void GivenGameStartedWhenPlayCardThenRaiseCardPlayed()
    {
        EventPublisherFake eventPublisher = new();
        var gameId = GameId.Generate();
        var game = new Game(new[] { new GameStarted(gameId, new Card(CardColor.Blue, CardValue.Zero)) });

        Card card = new(CardColor.Blue, CardValue.One);
        game.PlayCard(eventPublisher, card);

        Check.That(eventPublisher.Events)
            .ContainsExactly(new CardPlayed(gameId, card));
    }

    [TestMethod]
    public void GivenGameStartedWhenPlayWrongCardThenDoNotRaiseCardPlayed()
    {
        EventPublisherFake eventPublisher = new();
        var gameId = GameId.Generate();
        var game = new Game(new[] { new GameStarted(gameId, new Card(CardColor.Blue, CardValue.Zero)) });

        Card card = new(CardColor.Red, CardValue.One);
        game.PlayCard(eventPublisher, card);

        Check.That(eventPublisher.Events).IsEmpty();
    }

    [TestMethod]
    public void GivenCardPlayedWhenPlayCardThenRaiseCardPlayed()
    {
        EventPublisherFake eventPublisher = new();
        var gameId = GameId.Generate();
        var game = new Game(new IDomainEvent[]
        {
            new GameStarted(gameId, new Card(CardColor.Blue, CardValue.Zero)),
            new CardPlayed(gameId, new Card(CardColor.Blue, CardValue.One))
        });

        Card card = new(CardColor.Red, CardValue.One);
        game.PlayCard(eventPublisher, card);

        Check.That(eventPublisher.Events)
            .ContainsExactly(new CardPlayed(gameId, card));
    }

    [TestMethod]
    public void GivenCardPlayedWhenPlayWrongCardThenDoNotRaiseCardPlayed()
    {
        EventPublisherFake eventPublisher = new();
        var gameId = GameId.Generate();
        var game = new Game(new IDomainEvent[]
        {
            new GameStarted(gameId, new Card(CardColor.Blue, CardValue.Zero)),
            new CardPlayed(gameId, new Card(CardColor.Blue, CardValue.One))
        });

        Card card = new(CardColor.Red, CardValue.Zero);
        game.PlayCard(eventPublisher, card);

        Check.That(eventPublisher.Events).IsEmpty();
    }
}