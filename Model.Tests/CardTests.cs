using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.Enums;

namespace Model.Tests
{
    [TestClass]
    public class CardTests
    {

        [TestMethod]
        public void FindComplementaryCard_1()
        {
            var cardOne = new Card(NumberOfSymbols.One, Color.Red, Filling.Filled, SymbolType.Ellipse);
            var cardTwo = new Card(NumberOfSymbols.Two, Color.Red, Filling.Filled, SymbolType.Ellipse);

            var complementaryCard = cardOne.FindComplementaryCard(cardTwo);
            var expectedResult = new Card(NumberOfSymbols.Three, Color.Red, Filling.Filled, SymbolType.Ellipse);

            Assert.AreEqual(expectedResult, complementaryCard);
        }

        [TestMethod]
        public void FindComplementaryCard_2()
        {
            var cardOne = new Card(NumberOfSymbols.One, Color.Red, Filling.Filled, SymbolType.Ellipse);
            var cardTwo = new Card(NumberOfSymbols.Two, Color.Red, Filling.Filled, SymbolType.Ellipse);

            var complementaryCard = cardOne.FindComplementaryCard(cardTwo);
            var expectedResult = new Card(NumberOfSymbols.Three, Color.Red, Filling.Filled, SymbolType.Ellipse);

            Assert.AreEqual(expectedResult, complementaryCard);
        }

    }
}
