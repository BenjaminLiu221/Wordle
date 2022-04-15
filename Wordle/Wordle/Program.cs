using System;

namespace Wordle
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Wordle! You will be guessing the five letter Wordle. Please enter a five letter word and press Enter.");

            Wordle wordle = new Wordle();
            wordle.PlayGame();
        }
    }
}