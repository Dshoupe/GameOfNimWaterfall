using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfNim
{
    public class Game
    {
        public static Random randy = new Random();
        public static void StartGame()
        {
            bool gameContinue = true;
            do
            {
                Console.Clear();
                Console.WriteLine("Welcome to the WORLD OF NIM CHAMPIONSHIPS!!!!!\n");
                string Player1 = PromptForInput("Enter the first player's name: ");
                Console.WriteLine();
                string Player2 = PromptForInput("Enter the second player's name: ");

                int[] nim = 
                {
                    3,
                    5,
                    7
                };
                
                int playerTurn = randy.Next(1,101);
                do
                {
                    bool validInput = true;
                    int numTaken = 0;
                    int heap = 0;
                    
                    if (playerTurn % 2 == 0)//Player 2
                    {
                        Console.WriteLine($"\nIt is {Player2}'s turn\n");
                    }
                    else//Player 1
                    {
                        Console.WriteLine($"\nIt is {Player1}'s turn\n");
                    }
                    for (int i = 0; i < nim.Length; i++)
                    {
                        Console.WriteLine($"Heap {i+1}: {nim[i]}");
                    }
                    Console.WriteLine();
                    try
                    {
                        heap = int.Parse(PromptForInput("Which heap would you like to take from: "))-1;
                        validInput = heap >= 0 && heap <= 2;
                        if (validInput && nim[heap] == 0)
                        {
                            validInput = false;
                            Console.WriteLine("You must choose a heap with tokens to take.");
                        }
                        else if(!validInput)
                        {
                            Console.WriteLine("Please choose a valid heap.");
                        }
                    }
                    catch (FormatException)
                    {
                        validInput = false;
                        Console.WriteLine("You must give a number");
                    }
                    if (validInput)
                    {
                        try
                        {
                            numTaken = int.Parse(PromptForInput("How many tokens would you like to take: "));
                        }
                        catch (FormatException)
                        {
                            validInput = false;
                            Console.WriteLine("You must give a number");
                        }
                        validInput = numTaken > 0 && numTaken <= nim[heap];
                        if (validInput)
                        {
                            nim[heap] -= numTaken;
                            playerTurn++;
                        }
                        else
                        {
                            Console.WriteLine("You must choose a valid number of tokens");
                        }
                    }
                } while ((nim[0] + nim[1] + nim[2]) != 0);
                if (playerTurn-1 % 2 == 0)
                {
                    Console.WriteLine($"{Player1} is the winner!");
                }
                else
                {
                    Console.WriteLine($"{Player2} is the winner!");
                }
                gameContinue = PromptForInput("Would you like to play again? (y/n)").Equals("n", StringComparison.InvariantCultureIgnoreCase)? false : true;
            } while (gameContinue);
        }

        public static string PromptForInput(string prompt)
        {
            string userInput = "";
            bool isValid = false;
            do
            {
                Console.Write(prompt);
                userInput = Console.ReadLine();
                if (string.IsNullOrEmpty(userInput))
                {
                    Console.WriteLine("Please enter something");
                }
                else
                {
                    isValid = true;
                }

            } while (!isValid);
            return userInput;
        }
    }
}
