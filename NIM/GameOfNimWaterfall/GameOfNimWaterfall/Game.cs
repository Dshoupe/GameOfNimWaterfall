using CSC160_ConsoleMenu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfNimWaterfall.Models
{
    /// <summary>
    /// This class holds most game logic and runs through a basic game of Nim with either 2 human players or an AI player and a human player
    /// </summary>
    public static class Game
    {
        //Holds the heaps array so that every method can see it. heaps is a Heap array that holds the amount of tiles in each heap as well as the
        //length of the array for the amount of heaps
        public static Heap[] heaps;
        //Holds the players array so that every method has access to the 2 players. players is an array of Player that can hold 2 human players or an AI
        //player with a human as a gamemode
        private static Player[] players = new Player[2];
        //This random is here so that the GetRandom() method can access a static random
        private static Random rand = new Random();
        //This is a holder for the 4 different foreground colors a user can choose for the game colors. They have the options of Red, Magenta, Green,
        //and Cyan.
        private static ConsoleColor[] consoleColors = new ConsoleColor[] { ConsoleColor.Red, ConsoleColor.Magenta, ConsoleColor.Green, ConsoleColor.Cyan };
        //This holds the user's choice for the color and acts as the "index" of the consoleColors for easy interation and color gathering
        private static int gameColor = 1;

        /// <summary>
        /// GameSetup takes in nothing and returns void. Its job is to get user input for gamemode, difficulty, and gamecolor, it also holds the
        /// primary loop that the game runs through. Each menu is housed in a do while loop that will make sure the user selects a valid choice and 
        /// loops infinitely for the user's valid choice. This method runs the PlayGame() method once the user has successfully chosen the gamemode,
        /// gamecolor, and difficulty.
        /// </summary>
        public static void GameSetup()
        {
            bool exitGame = false;
            do
            {
                //This is the main menu, it has few options and acts as a home for when the user decides to exit the game. This is the only place 
                //the game may gracefully exit the game.
                Console.WriteLine("Welcome to the Game of Nim\nChoose an option to start:");
                int mainSelection = CIO.PromptForMenuSelection(new String[] { "Start Game", "View Instructions" }, true);

                switch (mainSelection)
                {
                    case 1:
                        bool exitMode = false;
                        do
                        {
                            //This is the game mode menu that uses CIO to get human vs human or computer vs human in int form, as well as instructions and
                            //a go back option
                            Console.WriteLine("\nChoose a gamemode: ");
                            int gameMode = CIO.PromptForMenuSelection(new String[] { "Human vs Computer", "Human vs Human", "Instructions", "Go Back" }, false);

                            switch (gameMode)
                            {
                                case 1: 
                                case 2:
                                    //This method call creates 2 characters, those character's types are based on gameMode, which is the GameMode Menu's return
                                    CreatePlayers(gameMode);
                                    //Game Color selection - It is setting class level variables of the console colors so other methods could see the
                                    //colors. We store four different colors in a ConsoleColor array for user selection.
                                    Console.WriteLine("What game color would you like to play on: ");
                                    gameColor = CIO.PromptForMenuSelection(new string[] { "Red", "Magenta", "Green", "Blue" }, false);
                                    Console.ForegroundColor = consoleColors[gameColor-1];

                                    //Initialize heapNums for the upcoming menu
                                    int[] heapNums = new int[] { 0 };
                                    bool exitDifficulty = false;
                                    do
                                    {
                                        //This menu prompts the user to select a game difficulty for the heaps to be created with. Either {3,3}, {2,5,7},
                                        //or {2,3,8,9} as heap ranges
                                        Console.WriteLine("\nChoose a difficulty: ");
                                        int difficultySelect = CIO.PromptForMenuSelection(new String[] { "Easy(3,3)", "Medium(2,5,7)", "Hard(2,3,8,9)", "Instructions", "Go Back" }, true);

                                        heapNums = new int[] { 0 };
                                        switch (difficultySelect)
                                        {
                                            case 1:
                                                //Easy
                                                heapNums = new int[] { 3, 3 };
                                                exitDifficulty = true;
                                                break;
                                            case 2:
                                                //Medium
                                                heapNums = new int[] { 2, 5, 7 };
                                                exitDifficulty = true;
                                                break;
                                            case 3:
                                                //Hard
                                                heapNums = new int[] { 2, 3, 8, 9 };
                                                exitDifficulty = true;
                                                break;
                                            case 4:
                                                //Method call for priniting instructions
                                                PrintInstructions();
                                                break;
                                            case 5:
                                                //Go back one menu layer, to the gamemode selection
                                                heapNums = new int[] { 0 };
                                                Console.ResetColor();
                                                exitDifficulty = true;
                                                break;
                                            case 0:
                                                //Exits to the main menu
                                                heapNums = new int[] { 0 };
                                                Console.ResetColor();
                                                exitMode = true;
                                                exitDifficulty = true;
                                                break;
                                        }
                                        //This is a method call to CreateHeaps() so that the heaps can be created for game usage. Takes in the int array 
                                        //for difficulty
                                        CreateHeaps(heapNums);
                                    } while (!exitDifficulty);

                                    //Play Game. This starts the game so long as the difficulty array isn't 0
                                    if (heapNums[0] != 0)
                                    {
                                        CreateHeaps(heapNums);
                                        PlayGame();
                                        exitMode = CIO.PromptForBool("Would you like to play again?(y/n)", "n", "y");
                                        Console.ResetColor();
                                    }

                                    break;
                                case 3:
                                    //Method call for priniting instructions
                                    PrintInstructions();
                                    break;
                                case 4:
                                    //Exits to the main menu
                                    exitMode = true;
                                    break;
                            }
                        } while (!exitMode);
                        break;
                    case 2:
                        //Method call for priniting instructions
                        PrintInstructions();
                        break;
                    case 0:
                        //Exits the program gracefully with a goodbye message
                        exitGame = true;
                        break;
                }
            } while (!exitGame);
            Console.WriteLine("Goodbye!");
        }

        public static int GetRandom(int max)
        {
            return rand.Next(1, max + 1);
        }

        private static void CreateHeaps(int[] heapDifficulty)
        {
            heaps = new Heap[heapDifficulty.Length];
            for (int i = 0; i < heapDifficulty.Length; i++)
            {
                heaps[i] = new Heap(heapDifficulty[i]);
            }
        }

        private static void CreatePlayers(int count)
        {
            if (count == 1)
            {
                players[0] = new HumanPlayer(CIO.PromptForInput("\nEnter your player's name: ", false));
                players[1] = new AIPlayer();
            }
            else
            {
                players[0] = new HumanPlayer(CIO.PromptForInput("Enter the first player's name: ", false));
                players[1] = new HumanPlayer(CIO.PromptForInput("\nEnter the second player's name: ", false));
            }
        }

        private static void PlayGame()
        {
            int playerTurn = GetRandom(100);
            bool gameOver = false;
            do
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                for (int i = 0; i < heaps.Length; i++)
                {
                    Console.Write($"Heap {i + 1}: {heaps[i].Tiles} - ");
                    for (int x = 0; x < heaps[i].Tiles; x++)
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.Write(" * ");
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }
                    Console.WriteLine();
                }
                Console.ForegroundColor = consoleColors[gameColor - 1];

                int[] playerHeap = null;
                Console.WriteLine($"It is {players[playerTurn % 2].Name}'s turn\n");
                playerHeap = players[playerTurn % 2].TakeTurn();

                if (playerHeap[0] == 0 && playerHeap[1] == 0)
                {
                    gameOver = true;
                }
                else
                {
                    heaps[playerHeap[0]].Tiles -= playerHeap[1];
                    playerTurn++;

                    int total = 0;
                    foreach (Heap i in heaps)
                    {
                        total += i.Tiles;
                    }
                    gameOver = (total == 0);
                    if (gameOver && total == 0)
                    {
                        Console.BackgroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("\t\t\t\t\t\t");
                        Console.WriteLine($"\t\t{players[(playerTurn % 2)].Name} is the winner!\t\t");
                        Console.WriteLine("\t\t\t\t\t\t");
                    }
                }
            } while (!gameOver);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine();
        }

        public static void PrintInstructions()
        {
            Console.WriteLine("\nInstructions:\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\nAt the start of the first player's turn (chosen randomly), they will take any number from 1 to the max heap count,\nfrom any heap, as long as that heap has a number greater than 0. After this the next player's turn is made.\nThis loop continues until there is only one move left to make, which will be to take the final tile from the heap,\nwhich makes this player the loser of the round.\n");
        }
    }
}