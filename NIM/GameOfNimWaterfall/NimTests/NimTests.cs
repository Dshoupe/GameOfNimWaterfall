using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GameOfNimWaterfall;
using GameOfNimWaterfall.Models;
using System.IO;

namespace NimTests
{
    [TestClass]
    public class NimTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
        "A userId of null was inappropriately allowed.")]
        public void NullUserIdInConstructor()
        {
            Player player = new HumanPlayer(null);
        }

        [TestMethod]
        public void ConsoleValidation()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                Game.GameSetup();

                string expected = string.Format("Ploeh{0}", Environment.NewLine);
                Assert.AreEqual(expected, sw.ToString());
            }
        }
    }
}
