using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfNimWaterfall.Models
{
    /// <summary>
    /// A AI implementation of the abstract parent, Player
    /// </summary>
    public class AIPlayer : Player
    {
        /// <summary>
        /// AI's implementation of TakeTurn(). It randomly generates the heapNumber and tileAmount and validates each random
        /// </summary>
        /// <returns></returns>
        public override int[] TakeTurn()
        {
            int heap = 0;
            do
            {
                heap = Game.GetRandom(Game.heaps.Count())-1;
            } while (Game.heaps[heap].Tiles == 0);

            int tileAmount = Game.GetRandom(Game.heaps[heap].Tiles);

            Console.WriteLine($"\nComputer took {tileAmount} tile(s) from heap {heap+1}\n");

            return new int[] { heap, tileAmount };
        }

        /// <summary>
        /// Contructs a new instance of AIPlayer with the name of "Computer"
        /// </summary>
        public AIPlayer()
        {
            Name = "Computer";
        }
    }
}