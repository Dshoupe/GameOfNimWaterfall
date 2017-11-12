using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfNimWaterfall.Models
{
    /// <summary>
    /// An absracted version of a Player, with no implemented code. The AIPlayer and HumanPlayer will implement this abstract class
    /// </summary>
    public abstract class Player
    {
        //Full Property for the player's name -- Can't be null
        private string name;

        public string Name
        {
            get { return name; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Name cannot be null");
                }
                else
                {
                    name = value;
                }
            }
        }

        /// <summary>
        /// Abstract version, each implemented version should prompt/generate an int[] of a heapNumber and a tileAmount
        /// </summary>
        /// <returns>An int[] of a heapNumber and a tileAmount</returns>
        public abstract int[] TakeTurn();
    }
}