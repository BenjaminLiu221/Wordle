using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Wordle
{
    class Wordle
    {
        public void PlayGame()
        {
            Random randomizer = new Random();

            int randomNumber = (int)randomizer.Next(0, 10);

            var wordleWord = WordleClass.GetWordle()[randomNumber].Word;
            Console.WriteLine($"Wordle is:{wordleWord}");

            string letterBoardRow1 = "Q W E R T Y U I O P";
            string letterBoardRow2 = "A S D F G H J K L";
            string letterBoardRow3 = "Z X C V B N M";
            
            string outputDisplayPadding = "";

            foreach (string row in wordleBoard)
            {
                Console.WriteLine($"{row,28}");
            }

            Console.WriteLine("");
            Console.WriteLine($"Letter Board: {letterBoardRow1}");
            Console.WriteLine($" {letterBoardRow2,31}");
            Console.WriteLine($" {letterBoardRow3,28}");

            Console.WriteLine("");
            Console.WriteLine("Enter 'Quit' to exit the game.");
            Console.WriteLine("");

            bool guessAgain = true;

            int guessAttemptsRemaining = 6;
            int useWordleBoardRow = 0;

            do
            {
                Console.Write("Guess here: ");
                string userGuess = Console.ReadLine().ToLower();

                int userGuessLength = userGuess.Length;

                int containInvalidCharCount = 0;

                bool userInputValidationPassed = true;

                bool wordleListValidationPassed = WordleClass.wordleList.Contains(userGuess); ;

                if (userGuess.Equals("quit"))
                {
                    return;
                }
                else
                {
                    if (userGuessLength != 5)
                    {
                        userInputValidationPassed = false;
                        Console.WriteLine("\"That is not a valid guess. Please enter a five letter word and then press Enter.\"");
                        Console.WriteLine("");
                    }
                    else 
                    {
                        for (int i = 0; i < userGuess.Length; i++)
                        {
                            if (char.IsDigit(userGuess[i]))
                            {
                                containInvalidCharCount++;
                            }
                        }
                        if (containInvalidCharCount == 0)
                        {
                            Regex RgxUrl = new Regex("[^a-z0-9]");
                            if (RgxUrl.IsMatch(userGuess))
                            {
                                containInvalidCharCount++;
                            }
                        }
                        if (containInvalidCharCount > 0)
                        {
                            userInputValidationPassed = false;
                            Console.WriteLine("\"That is not a valid guess. Your input contains a number(s) or special character(s). Please enter a five letter word and then press Enter.\"");
                            Console.WriteLine("");
                        }
                    }
                }

                if (userInputValidationPassed)
                {
                    if (wordleListValidationPassed)
                    {

                        if (userGuess != wordleWord)
                        {
                            guessAttemptsRemaining--;

                            wordleBoard[useWordleBoardRow] = userGuess;
                            useWordleBoardRow++;
                        }
                        else
                        {
                            wordleBoard[useWordleBoardRow] = userGuess;
                            useWordleBoardRow++;

                            foreach (string wordleRow in wordleBoard)
                            {
                                if (wordleRow.Equals("_ _ _ _ _"))
                                {
                                    Console.WriteLine($"{wordleRow,28}");
                                }
                                else
                                {
                                    Console.Write($"{outputDisplayPadding,19}");
                                    displayWordleBoard(wordleWord, wordleRow);
                                }
                            }

                            Console.WriteLine("\"You have guessed the Wordle. Thank you for playing!\"");
                            return;
                        }
                    }
                    else
                    {
                        Console.WriteLine("\"This word is not in the word list. Please enter a five letter word and then press Enter.\"");
                    }
                }

                if (guessAttemptsRemaining == 0)
                {
                    Console.WriteLine($"\"You have 0 guesses remaining. The wordle was \"{wordleWord.ToUpper()}\".Thank you for playing. Good Bye!\"");
                    return;
                }
                else
                {
                    Console.WriteLine($"Guess Attempt(s) Remaining: {guessAttemptsRemaining}");

                    foreach (string wordleRow in wordleBoard)
                    {
                        if (wordleRow.Equals("_ _ _ _ _"))
                        {
                            Console.WriteLine($"{wordleRow,28}");
                        }
                        else
                        {
                            Console.Write($"{outputDisplayPadding,19}");
                            displayWordleBoard(wordleWord, wordleRow);
                        }
                    }
                }
            } while (guessAgain);
        }
        public string BuildOutputDisplay(char wordleWordChar, char userGuessChar, string outputDisplayChar, string finalOutputDisplayChar)
        {
            if ((outputDisplayChar == "_") && (wordleWordChar == userGuessChar))
            {
                finalOutputDisplayChar = userGuessChar.ToString();
            }
            else
            {
                finalOutputDisplayChar = outputDisplayChar;
            }
            return finalOutputDisplayChar;
        }

        public string letterBoardChar(string letterBoardChar, char wordleWordChar, char userGuessChar)
        {
            if (wordleWordChar != userGuessChar)
            {
                letterBoardChar = userGuessChar.ToString();
            }
            else
            {
                letterBoardChar = " ";
            }
            return letterBoardChar;
        }

        public static List<string> wordleBoard = new List<string>
        {
            "_ _ _ _ _",
            "_ _ _ _ _",
            "_ _ _ _ _",
            "_ _ _ _ _",
            "_ _ _ _ _",
            "_ _ _ _ _"
        };

        private void displayWordleBoard(string wordleWord, string userGuess)
        {
            for (int i = 0; i < userGuess.Length; i++)
            {
                if (wordleWord.Contains(userGuess[i]))
                {
                    if (wordleWord[i] == userGuess[i])
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write($"{userGuess[i]} ");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write($"{userGuess[i]} ");
                        Console.ResetColor();
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write($"{userGuess[i]} ");
                    Console.ResetColor();
                }
            }
            Console.WriteLine("");
        }
    }
}