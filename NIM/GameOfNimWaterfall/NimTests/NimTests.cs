using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GameOfNimWaterfall;
using GameOfNimWaterfall.Models;

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
        public void Test1()
        {
            //using (stringW)
        }
    }
}
