using Microsoft.VisualStudio.TestTools.UnitTesting;
using NFluent;
using Un.Domain.Game;

namespace Un.Domain.Tests;

[TestClass]
public class GameIdTest
{
    [TestMethod]
    public void WhenGenerate2IdThenIsNotEquals()
    {
        var id1 = GameId.Generate();
        var id2 = GameId.Generate();

        Check.That(id1).IsNotEqualTo(id2);
    }
}