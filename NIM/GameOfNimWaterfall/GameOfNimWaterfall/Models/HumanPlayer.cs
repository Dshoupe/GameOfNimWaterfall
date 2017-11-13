using CSC160_ConsoleMenu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfNimWaterfall.Models
{
    /// <summary>
    /// A human implementation of the abstract parent, Player
    /// </summary>
    public class HumanPlayer : Player
    {
        /// <summary>
        /// Human player's implementation of TakeTurn(). It will prompt the user infinitly until the user enters valid information
        /// </summary>
        /// <returns>An int[] of heapNumber and tileAmount</returns>
        public override int[] TakeTurn()
        {
            int heap = 0;
            int tileAmount = 0;
            bool isDone = false;
            do
            {
                //A menu allowing the user to see the instructions or quit as well as its turn
                int userChoice = CIO.PromptForMenuSelection(new string[] { "Take Turn", "Instructions" }, true);
                switch (userChoice)
                {
                    case 1:
                        do
                        {
                            heap = CIO.PromptForInt($"Enter a heap (1 - {Game.heaps.Count()}): ", 1, Game.heaps.Count())-1;
                            if (Game.heaps[heap].Tiles == 0)
                            {
                                Console.WriteLine($"Heap {heap} is empty, choose another heap with tiles in it.");
                            }
                        } while (Game.heaps[heap].Tiles == 0);

                        tileAmount = CIO.PromptForInt($"How many tiles would you like to take (1 - {Game.heaps[heap].Tiles}): ", 1, Game.heaps[heap].Tiles);

                        isDone = CIO.PromptForBool($"Are you sure that you would like to take {tileAmount} tile(s) from heap {heap + 1}?(y/n)", "y", "n");
                        break;
                    case 2:
                        Game.PrintInstructions();
                        break;
                    case 0:
                        isDone = true;
                        break;
                }
            } while (!isDone);

            if (heap != 0 && tileAmount != 0)
            {
                Console.WriteLine($"\n{Name} took {tileAmount} tile(s) from heap {heap + 1}\n");
            }

            return new int[] { heap, tileAmount };
        }

        /// <summary>
        /// Constructor that takes in the player's name. Sets the property
        /// </summary>
        /// <param name="name">The current player's name</param>
        public HumanPlayer(string name)
        {
            Name = name;
        }
    }
}