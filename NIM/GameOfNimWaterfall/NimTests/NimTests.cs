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
