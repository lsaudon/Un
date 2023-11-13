using Microsoft.VisualStudio.TestTools.UnitTesting;
using NFluent;
using Un.Domain.Games;
using Un.Domain.Games.Handlers;
using Un.Infrastructure;

namespace Un.Application.Tests;

[TestClass]
public class UnTest
{
  [TestMethod]
  public void Truc()
  {
    EventsStore eventsStore = new();
    EventPublisher eventPublisher = new();
    EventPublisherWithStorage eventPublisherWithStorage = new(eventsStore, eventPublisher);
    GameStateRepository gameStateRepository = new();
    eventPublisher.Subscribe(new UpdateState(gameStateRepository));
    GameId gameId = Game.Start(eventPublisherWithStorage);
    new Game(eventsStore.GetEventsOfAggregate(gameId))
     .PlayCard(eventPublisherWithStorage, PlayerId.Generate(), new Card(CardColor.Blue, CardValue.One));
    GameStateProjection stateOfGame = gameStateRepository.GetStateOfGame(gameId);
    Check.That(stateOfGame.Card).IsEqualTo(new Card(CardColor.Blue, CardValue.One));
  }
}