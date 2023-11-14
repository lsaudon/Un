using FsCheck;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NFluent;
using Un.Domain.Games;
using Un.Domain.Games.Handlers;
using Un.Infrastructure;
using Check = NFluent.Check;

namespace Un.Application.Tests;

public static class CardArbitrary
{
  public static Arbitrary<Card> Generate() => Arb.From(
                                                       from color in Arb.Generate<CardColor>()
                                                       from value in color == CardColor.Blue
                                                                       ? Arb.Generate<CardValue>()
                                                                       : Gen.Constant(CardValue.Zero)
                                                       select new Card(color, value)
                                                      );
}

[TestClass]
public class UnTest
{
  [TestMethod]
  public void GivenNewGameWhenPlayingCardThenGameStateShouldReflectCardPlayed()
  {
    Prop.ForAll(CardArbitrary.Generate(), Body).QuickCheckThrowOnFailure();
    return;

    void Body(Card card)
    {
      EventsStore eventsStore = new();
      EventPublisher eventPublisher = new();
      EventPublisherWithStorage eventPublisherWithStorage = new(eventsStore, eventPublisher);
      GameStateRepository gameStateRepository = new();
      eventPublisher.Subscribe(new UpdateState(gameStateRepository));

      GameId gameId = Game.Start(eventPublisherWithStorage);
      new Game(eventsStore.GetEventsOfAggregate(gameId)).PlayCard(eventPublisherWithStorage, PlayerId.Generate(), card);

      GameStateProjection stateOfGame = gameStateRepository.GetStateOfGame(gameId);
      Check.That(stateOfGame.Card)
           .IsEqualTo(card);
    }
  }
}