using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfNimWaterfall.Models
{
    public class Heap
    {
        public int Tiles { get; set; }

        public Heap(int tiles)
        {
            Tiles = tiles;
        }
    }
}