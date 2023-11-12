using Microsoft.VisualStudio.TestTools.UnitTesting;
using NFluent;
using Un.Domain.Games;

namespace Un.Domain.Tests.Games;

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
    
    [TestMethod]
    public void WhenToStringIdThenId()
    {
        var id = GameId.Generate();

        Check.That(id.ToString()).IsEqualTo(id.Id);
    }
}