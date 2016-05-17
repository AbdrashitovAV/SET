using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Model.Tests
{
    [TestClass]
    public class DeckGeneratorTests
    {

        [TestMethod]
        public void test1()
        {
            var a = new DeckGenerator();
            a.GetDeck(true);
        }
    }
}
