using Microsoft.VisualStudio.TestTools.UnitTesting;
using NFluent;
using Un.Domain.Games;
using Un.Domain.Games.Handlers;
using Un.Infrastructure;

namespace Un.Application.Tests;

[TestClass]
public class UnTest
{
  private readonly EventsStore _eventsStore = new();
  private readonly EventPublisher _eventPublisher = new();
  private readonly EventPublisherWithStorage _eventPublisherWithStorage;
  private readonly GameStateRepository _gameStateRepository = new();

  public UnTest()
  {
    _eventPublisherWithStorage = new EventPublisherWithStorage(_eventsStore, _eventPublisher);
    _eventPublisher.Subscribe(new UpdateState(_gameStateRepository));
  }

  [TestMethod]
  public void GivenNewGameWhenPlayingCardThenGameStateShouldReflectCardPlayed()
  {
    GameId gameId = Game.Start(_eventPublisherWithStorage);
    new Game(_eventsStore.GetEventsOfAggregate(gameId))
     .PlayCard(_eventPublisherWithStorage, PlayerId.Generate(), new Card(CardColor.Blue, CardValue.One));

    GameStateProjection stateOfGame = _gameStateRepository.GetStateOfGame(gameId);
    Check.That(stateOfGame.Card).IsEqualTo(new Card(CardColor.Blue, CardValue.One));
  }
}