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
        Check.That(evt.LastCard).Is(new Card(CardColor.Blue, CardValue.Zero));
    }
}