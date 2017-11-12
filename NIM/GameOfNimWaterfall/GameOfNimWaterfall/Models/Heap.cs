using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfNimWaterfall.Models
{
    /// <summary>
    /// A model for the heap that holds the amount of tiles that heap has
    /// </summary>
    public class Heap
    {
        /// <summary>
        /// A int value for the number of tiles the heap has
        /// </summary>
        public int Tiles { get; set; }

        /// <summary>
        /// Contructs a heap with a tile amount
        /// </summary>
        /// <param name="tiles">Amount of tiles the heap will contain</param>
        public Heap(int tiles)
        {
            Tiles = tiles;
        }
    }
}