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

            int randomNumber = (int)randomizer.Next(0, 1000);

            var wordleWord = WordleClass.GetWordle()[randomNumber].Word;
            //Console.WriteLine($"Wordle is:{wordleWord}");

            var letterBoard = new Dictionary<char, string>();

            var letterBoardStr = "QWERTYUIOPASDFGHJKLZXCVBNM";

            foreach(char letter in letterBoardStr)
            {
                letterBoard.Add(letter, "black");
            }
            
            string outputDisplayPadding = "";

            foreach (string row in wordleBoard)
            {
                Console.WriteLine($"{row,28}");
            }
            Console.WriteLine("");

            displayLetterBoard(letterBoard, outputDisplayPadding);

            Console.WriteLine("");
            Console.WriteLine("Enter 'Quit' to exit the game.");

            bool guessAgain = true;

            int guessAttemptsRemaining = 6;
            int useWordleBoardRow = 0;

            do
            {
                Console.WriteLine("");
                Console.Write("Guess here: ");
                string userGuess = Console.ReadLine().ToLower();

                //Console.Clear();

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

                            updateLetterBoard(letterBoard, wordleWord, userGuess);
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

                            updateLetterBoard(letterBoard, wordleWord, userGuess);

                            displayLetterBoard(letterBoard, outputDisplayPadding);

                            Console.WriteLine("");
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
                    displayLetterBoard(letterBoard, outputDisplayPadding);
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

                    displayLetterBoard(letterBoard, outputDisplayPadding);
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

        private void displayWordleBoard(string _wordleWord, string _userGuess)
        {
            _wordleWord = _wordleWord.ToUpper();
            _userGuess = _userGuess.ToUpper();
            for (int i = 0; i < _userGuess.Length; i++)
            {
                if (_wordleWord.Contains(_userGuess[i]))
                {
                    if (_wordleWord[i] == _userGuess[i])
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write($"{_userGuess[i]} ");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write($"{_userGuess[i]} ");
                        Console.ResetColor();
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write($"{_userGuess[i]} ");
                    Console.ResetColor();
                }
            }
            Console.WriteLine("");
        }

        private void updateLetterBoard(Dictionary<char, string> _letterBoard, string _wordleWord, string _userGuess)
        {
            _wordleWord = _wordleWord.ToUpper();
            _userGuess = _userGuess.ToUpper();
            for (int i = 0; i < _userGuess.Length; i++)
            {
                if (_wordleWord.Contains(_userGuess[i]))
                {
                    if (_wordleWord[i] == _userGuess[i])
                    {
                        _letterBoard[_userGuess[i]] = "green";
                    }
                    else
                    {
                        _letterBoard[_userGuess[i]] = "yellow";
                    }
                }
                else
                {
                    _letterBoard[_userGuess[i]] = "red";
                }
            }
            Console.WriteLine("");
        }

        private void displayLetterBoard(Dictionary<char, string> _letterBoard, string _outputDisplayPadding)
        {
            Console.Write($"{_outputDisplayPadding,14}");
            foreach (var row in _letterBoard)
            {
                if (row.Key.Equals("P".ToCharArray()[0]))
                {
                    if (row.Value.Equals("red"))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    else if (row.Value.Equals("green"))
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    else if (row.Value.Equals("yellow"))
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }
                    Console.WriteLine($"{row.Key} ");
                    Console.ResetColor();
                    Console.Write($"{_outputDisplayPadding,15}");
                }
                else if (row.Key.Equals("L".ToCharArray()[0]))
                {
                    if (row.Value.Equals("red"))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    else if (row.Value.Equals("green"))
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    else if (row.Value.Equals("yellow"))
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }
                    Console.WriteLine($"{row.Key} ");
                    Console.ResetColor();
                    Console.Write($"{_outputDisplayPadding,16}");
                }
                else
                {
                    if (row.Value.Equals("red"))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    else if (row.Value.Equals("green"))
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    else if (row.Value.Equals("yellow"))
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }
                    Console.Write($"{row.Key} ");
                    Console.ResetColor();
                }
            }
            Console.WriteLine("");
        }
    }
}