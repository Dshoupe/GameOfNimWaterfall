using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GameOfNimWaterfall;
using GameOfNimWaterfall.Models;
using System.IO;
using System.Linq;
using System.Threading;

namespace NimTests
{
    [TestClass]
    public class NimTests
    {
       /**
         * Known Tests:
         * GameSetup:
         *      -Foreground colors equal what the user chose
         *      -Make sure heap numbers are correct
         *      -Make sure heap tile amounts are correct
         *      -Quit goes back to main menu
         *      -Go back goes to last menu called
         *      -Play again prompt goes to either main menu or gamemode select menu depending on y/n
         *      -Make sure console output displays the instructions
         * 
         * PlayGame
         *      -PlayerTurn is random
         *      -Displays heap correctly
         *      -Chooses the correct player for first round
         *      -Prints out correct player's name
         *      -Exit method works properly mid game
         *      -Game ends with no tiles left
         *      -Proper winner is chosen at game end
         *      -Make sure console output displays the instructions
         *      -Player turn swaps after each turn
         *      -Make sure playerTurn affects the heap display and class level variable heaps is correct updated
         *      
         * ConsoleIO takes care of user input
         * 
         * CreatePlayers
         *      -Correct # of players
         *      -Correct Player names
         *      -Default human player names are "Player 1" and "Player 2"
         *      -AI's name is always "Computer"
         *      -Player class level array is set at end
         *      -Make sure console output displays the instructions
         *      
         * CreateHeaps
         *      -Make sure class level heaps are being set to the correct instances of heaps
         *      -TileAmount is correct
         *      
         * PrintInstructions
         *      -Make sure console output displays the instructions
         *      
         * GetRandom
         *      -Check inslusive min
         *      -Check inclusive max
         *      -Check if its in the range
         *      -Check outside ranges
         *      -Check to make sure max isn't negative
         *      
         * AI TakeTurn
         *      -Heap number random generates correct random values
         *      -Heap tile random generates correct random values
         *      -Returns an int array of heap number and heap tiles
         *      -Returns an int array of heap number and heap tiles IN ORDER
         * 
         * Human TakeTurn
         *      -Make sure console output displays the instructions
         *      -Selecting empty/null heaps are invalid
         *      -Displays the empty heap selection prompt
         *      -Player can verify their turn
         *      -Can quit mid selection
         *      -Returns an int array of heap number and heap tiles
         *      -Returns an int array of heap number and heap tiles IN ORDER
         **/


        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
        "A userId of null was inappropriately allowed.")]
        public void NullUserIdInConstructor()
        {
            Player player = new HumanPlayer(null);
        }

        //[TestMethod]
        //public void ConsoleValidation()
        //{
        //    using (StringWriter sw = new StringWriter())
        //    {
        //        Console.SetOut(sw);

        //        Game.GameSetup();

        //        string expected = string.Format("Ploeh{0}", Environment.NewLine);
        //        Assert.AreEqual(expected, sw.ToString());
        //    }
        //}
        [TestMethod]
        public void CreateHeapsEasyMode()
        {
            int[] expectedInput = new int[] { 3, 3 };
            Heap[] expectedOutcome = { new Heap(3), new Heap(3) };
            Game.CreateHeaps(expectedInput);
            Assert.AreEqual(expectedOutcome[0].Tiles, Game.heaps[0].Tiles);
            Assert.AreEqual(expectedOutcome[1].Tiles, Game.heaps[1].Tiles);
        }

        [TestMethod]
        public void CreateHeapsMediumMode()
        {
            int[] expectedInput = new int[] { 2, 5, 7 };
            Heap[] expectedOutcome = { new Heap(2), new Heap(5), new Heap(7) };
            Game.CreateHeaps(expectedInput);
            Assert.AreEqual(expectedOutcome[0].Tiles, Game.heaps[0].Tiles);
            Assert.AreEqual(expectedOutcome[1].Tiles, Game.heaps[1].Tiles);
            Assert.AreEqual(expectedOutcome[2].Tiles, Game.heaps[2].Tiles);
        }

        [TestMethod]
        public void CreateHeapsHardMode()
        {
            int[] expectedInput = new int[] { 2, 3, 8, 9 };
            Heap[] expectedOutcome = { new Heap(2), new Heap(3), new Heap(8), new Heap(9) };
            Game.CreateHeaps(expectedInput);
            Assert.AreEqual(expectedOutcome[0].Tiles, Game.heaps[0].Tiles);
            Assert.AreEqual(expectedOutcome[1].Tiles, Game.heaps[1].Tiles);
            Assert.AreEqual(expectedOutcome[2].Tiles, Game.heaps[2].Tiles);
            Assert.AreEqual(expectedOutcome[3].Tiles, Game.heaps[3].Tiles);
        }
        [TestMethod]
        public void RandomizerTestingLowerBoundInclusive()
        {
            int[] output = new int[100];
            for (int i = 0; i < 100; i++)
            {
                output[i] = Game.GetRandom(10);
            }
            Assert.IsTrue(output.Where(x => x < 1).Count() == 0);
        }

        [TestMethod]
        public void RandomizerTestingUpperBoundInclusive()
        {
            int[] output = new int[100];
            for (int i = 0; i < 100; i++)
            {
                output[i] = Game.GetRandom(10);
            }
            Assert.IsTrue(output.Where(x => x > 10).Count() == 0);
        }

        [TestMethod]
        public void RandomizerTestingInclusiveMax()
        {
            int[] output = new int[100];
            for (int i = 0; i < 100; i++)
            {
                output[i] = Game.GetRandom(10);
            }
            Assert.IsTrue(output.Where(x => x == 10).Count() > 0);
        }

        [TestMethod]
        public void RandomizerTestingInclusiveMin()
        {
            int[] output = new int[100];
            for (int i = 0; i < 100; i++)
            {
                output[i] = Game.GetRandom(10);
            }
            Assert.IsTrue(output.Where(x => x == 1).Count() > 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException),
        "A userId of null was inappropriately allowed.")]
        public void RandomizerNegativeMaxValue()
        {
            int randomInt = Game.GetRandom(-1);
        }

        [TestMethod]
        public void HumanPlayerNameGamemode1()
        {
            using (StringWriter sw = new StringWriter())
            {
                using (StringReader sr = new StringReader("Test"))
                {
                    Console.SetOut(sw);
                    Console.SetIn(sr);
                    Game.CreatePlayers(1);
                }
            }
            Assert.AreEqual("Test", Game.players[0].Name);
        }

        [TestMethod]
        public void HumanPlayerNameGamemode2()
        {
            using (StringWriter sw = new StringWriter())
            {
                using (StringReader sr = new StringReader("Test\nTest2"))
                {
                    Console.SetOut(sw);
                    Console.SetIn(sr);
                    Game.CreatePlayers(2);
                }
            }
            Assert.AreEqual("Test", Game.players[0].Name);
            Assert.AreEqual("Test2", Game.players[1].Name);
        }

        [TestMethod]
        public void HumanPlayer1DefaultNameGameMode1()
        {
            using (StringWriter sw = new StringWriter())
            {
                using (StringReader sr = new StringReader(" "))
                {
                    Console.SetOut(sw);
                    Console.SetIn(sr);
                    Game.CreatePlayers(1);
                }
            }
            Assert.AreEqual("Player", Game.players[0].Name);
        }

        [TestMethod]
        public void HumanPlayer1DefaultNameGameMode2()
        {
            using (StringWriter sw = new StringWriter())
            {
                using (StringReader sr = new StringReader(" \n "))
                {
                    Console.SetOut(sw);
                    Console.SetIn(sr);
                    Game.CreatePlayers(2);
                }
            }
            Assert.AreEqual("Player 1", Game.players[0].Name);
            Assert.AreEqual("Player 2", Game.players[1].Name);
        }
        [TestMethod]
        public void AIPlayerNameGamemode1()
        {
            using (StringWriter sw = new StringWriter())
            {
                using (StringReader sr = new StringReader("Test"))
                {
                    Console.SetOut(sw);
                    Console.SetIn(sr);
                    Game.CreatePlayers(1);
                }
            }
            Assert.AreEqual("Computer", Game.players[1].Name);
        }
        /*AI TakeTurn
         *      -Heap number random generates correct random values
         */
         [TestMethod]
        public void AIMakesLegalMoves()
        {
            AIPlayer ai = new AIPlayer();
            int[] output = new int[2];
            int passed = 0;
            
            for (int i = 0; i < 1000; i++)
            {
                Game.CreateHeaps(new int[] { 2, 3, 8, 9 });
                output = ai.TakeTurn();
                if(output[0] <= Game.heaps.Count() 
                    && output[0] >= 1 
                    && output[1] <= Game.heaps[output[0]].Tiles 
                    && output[1] >= 1)
                {
                    passed++;
                }
                else
                {
                    Console.WriteLine(output[0] + ", " + output[1]);
                }
            }
            Assert.IsTrue(passed == 1000);
        }

        private string color = "";
        private string difficulty = "";

        //1\n2\nTester1\nTester2\n1\n1\n1\n1\n3\ny\n1\n2\n3\ny\nT\nn\n0
        [TestMethod]
        public void ForegroundCheck1()
        {
            color = "1";
            Thread thread = new Thread(ColorTest);
            thread.Start();
            Thread.Sleep(2000);
            Assert.AreEqual(ConsoleColor.Red, Console.ForegroundColor);
            thread.Abort();
        }

        [TestMethod]
        public void ForegroundCheck2()
        {
            color = "2";
            Thread thread = new Thread(ColorTest);
            thread.Start();
            Thread.Sleep(2000);
            Assert.AreEqual(ConsoleColor.Magenta, Console.ForegroundColor);
            thread.Abort();
        }

        [TestMethod]
        public void ForegroundCheck3()
        {
            color = "3";
            Thread thread = new Thread(ColorTest);
            thread.Start();
            Thread.Sleep(2000);
            Assert.AreEqual(ConsoleColor.Green, Console.ForegroundColor);
            thread.Abort();
        }

        [TestMethod]
        public void ForegroundCheck4()
        {
            color = "4";
            Thread thread = new Thread(ColorTest);
            thread.Start();
            Thread.Sleep(2000);
            Assert.AreEqual(ConsoleColor.Cyan, Console.ForegroundColor);
            thread.Abort();
        }

        public void ColorTest()
        {
            using (StringWriter sw = new StringWriter())
            {
                using (StringReader sr = new StringReader($"1\n2\nTester1\nTester2\n{color}"))
                {
                    Console.SetOut(sw);
                    Console.SetIn(sr);

                    Game.GameSetup();
                }
            }
        }

        [TestMethod]
        public void HeapCheck1()
        {
            difficulty = "1";
            Thread thread = new Thread(HeapTest);
            thread.Start();
            Thread.Sleep(2000);
            Assert.AreEqual(3, Game.heaps[0].Tiles);
            Assert.AreEqual(3, Game.heaps[1].Tiles);
        }

        [TestMethod]
        public void HeapCheck2()
        {
            difficulty = "2";
            Thread thread = new Thread(HeapTest);
            thread.Start();
            Thread.Sleep(2000);
            Assert.AreEqual(2, Game.heaps[0].Tiles);
            Assert.AreEqual(5, Game.heaps[1].Tiles);
            Assert.AreEqual(7, Game.heaps[2].Tiles);
        }

        [TestMethod]
        public void HeapCheck3()
        {
            difficulty = "3";
            Thread thread = new Thread(HeapTest);
            thread.Start();
            Thread.Sleep(2000);
            Assert.AreEqual(2, Game.heaps[0].Tiles);
            Assert.AreEqual(3, Game.heaps[1].Tiles);
            Assert.AreEqual(8, Game.heaps[2].Tiles);
            Assert.AreEqual(9, Game.heaps[3].Tiles);
        }

        public void HeapTest()
        {
            using (StringWriter sw = new StringWriter())
            {
                using (StringReader sr = new StringReader($"1\n2\nTester1\nTester2\n1\n{difficulty}"))
                {
                    Console.SetOut(sw);
                    Console.SetIn(sr);

                    Game.GameSetup();
                }
            }
        }

        [TestMethod]
        public void PlayerTurnCheck()
        {
            for (int i = 0; i < 1000; i++)
            {
                int mod = Game.GetRandom(100) % 2;

                if (mod == 1)
                {
                    Assert.AreEqual(1, mod);
                }
                else
                {
                    Assert.AreEqual(0, mod);
                }
            }
        }
        /*
         *      -Heap tile random generates correct random values
         *      -Returns an int array of heap number and heap tiles
         *      -Returns an int array of heap number and heap tiles IN ORDER
         * 
         * Human TakeTurn
         *      -Make sure console output displays the instructions
         *      -Selecting empty/null heaps are invalid
         *      -Displays the empty heap selection prompt
         *      -Player can verify their turn
         *      -Can quit mid selection
         *      -Returns an int array of heap number and heap tiles
         *      -Returns an int array of heap number and heap tiles IN ORDER
         */

    }
}
