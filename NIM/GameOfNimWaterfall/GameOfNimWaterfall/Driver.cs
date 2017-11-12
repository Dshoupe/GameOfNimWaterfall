using GameOfNimWaterfall.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfNimWaterfall
{
    public class Driver
    {
        /// <summary>
        /// Main method, entry point of the program, runs GameSetup() to start game
        /// </summary>
        /// <param name="args">Not Used</param>
        public static void Main(string[] args)
        {
            Game.GameSetup();
        }
    }
}