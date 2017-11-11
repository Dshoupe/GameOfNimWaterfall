using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfNimWaterfall.Models
{
    public class AIPlayer : Player
    {
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

        public AIPlayer()
        {
            Name = "Computer";
        }
    }
}