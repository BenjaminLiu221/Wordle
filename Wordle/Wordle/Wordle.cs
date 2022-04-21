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
            //Console.WriteLine($"Wordle is:{wordleWord}");

            var wordleWordCharOne = wordleWord.ToCharArray()[0];
            var wordleWordCharTwo = wordleWord.ToCharArray()[1];
            var wordleWordCharThree = wordleWord.ToCharArray()[2];
            var wordleWordCharFour = wordleWord.ToCharArray()[3];
            var wordleWordCharFive = wordleWord.ToCharArray()[4];

            string outputCharOneDisplay = "_";
            string outputCharTwoDisplay = "_";
            string outputCharThreeDisplay = "_";
            string outputCharFourDisplay = "_";
            string outputCharFiveDisplay = "_";

            string letterBoardRow1 = "Q W E R T Y U I O P";
            string letterBoardRow2 = "A S D F G H J K L";
            string letterBoardRow3 = "Z X C V B N M";

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

                bool wordleListValidationPassed = WordleClass.wordleList.Contains(userGuess);

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
                    var userGuessCharOne = userGuess.ToCharArray()[0];
                    var userGuessCharTwo = userGuess.ToCharArray()[1];
                    var userGuessCharThree = userGuess.ToCharArray()[2];
                    var userGuessCharFour = userGuess.ToCharArray()[3];
                    var userGuessCharFive = userGuess.ToCharArray()[4];

                    var outputDisplayCharOneToString = "_";
                    var outputDisplayCharTwoToString = "_";
                    var outputDisplayCharThreeToString = "_";
                    var outputDisplayCharFourToString = "_";
                    var outputDisplayCharFiveToString = "_";

                    string incorrectLetterBoardCharOne = "";
                    string incorrectLetterBoardCharTwo = "";
                    string incorrectLetterBoardCharThree = "";
                    string incorrectLetterBoardCharFour = "";
                    string incorrectLetterBoardCharFive = "";

                    if (wordleListValidationPassed)
                    {

                        if (userGuess != wordleWord)
                        {
                            guessAttemptsRemaining--;

                            outputCharOneDisplay = BuildOutputDisplay(wordleWordCharOne, userGuessCharOne, outputDisplayCharOneToString, outputCharOneDisplay);
                            outputCharTwoDisplay = BuildOutputDisplay(wordleWordCharTwo, userGuessCharTwo, outputDisplayCharTwoToString, outputCharTwoDisplay);
                            outputCharThreeDisplay = BuildOutputDisplay(wordleWordCharThree, userGuessCharThree, outputDisplayCharThreeToString, outputCharThreeDisplay);
                            outputCharFourDisplay = BuildOutputDisplay(wordleWordCharFour, userGuessCharFour, outputDisplayCharFourToString, outputCharFourDisplay);
                            outputCharFiveDisplay = BuildOutputDisplay(wordleWordCharFive, userGuessCharFive, outputDisplayCharFiveToString, outputCharFiveDisplay);

                            incorrectLetterBoardCharOne = letterBoardChar(incorrectLetterBoardCharOne, wordleWordCharOne, userGuessCharOne);
                            incorrectLetterBoardCharTwo = letterBoardChar(incorrectLetterBoardCharTwo, wordleWordCharTwo, userGuessCharTwo);
                            incorrectLetterBoardCharThree = letterBoardChar(incorrectLetterBoardCharThree, wordleWordCharThree, userGuessCharThree);
                            incorrectLetterBoardCharFour = letterBoardChar(incorrectLetterBoardCharFour, wordleWordCharFour, userGuessCharFour);
                            incorrectLetterBoardCharFive = letterBoardChar(incorrectLetterBoardCharFive, wordleWordCharFive, userGuessCharFive);

                            letterBoardRow1 = letterBoard(letterBoardRow1, incorrectLetterBoardCharOne, incorrectLetterBoardCharTwo, incorrectLetterBoardCharThree, incorrectLetterBoardCharFour, incorrectLetterBoardCharFive);
                            letterBoardRow2 = letterBoard(letterBoardRow2, incorrectLetterBoardCharOne, incorrectLetterBoardCharTwo, incorrectLetterBoardCharThree, incorrectLetterBoardCharFour, incorrectLetterBoardCharFive);
                            letterBoardRow3 = letterBoard(letterBoardRow3, incorrectLetterBoardCharOne, incorrectLetterBoardCharTwo, incorrectLetterBoardCharThree, incorrectLetterBoardCharFour, incorrectLetterBoardCharFive);
                        }
                        else
                        {
                            outputCharOneDisplay = BuildOutputDisplay(wordleWordCharOne, userGuessCharOne, outputDisplayCharOneToString, outputCharOneDisplay);
                            outputCharTwoDisplay = BuildOutputDisplay(wordleWordCharTwo, userGuessCharTwo, outputDisplayCharTwoToString, outputCharTwoDisplay);
                            outputCharThreeDisplay = BuildOutputDisplay(wordleWordCharThree, userGuessCharThree, outputDisplayCharThreeToString, outputCharThreeDisplay);
                            outputCharFourDisplay = BuildOutputDisplay(wordleWordCharFour, userGuessCharFour, outputDisplayCharFourToString, outputCharFourDisplay);
                            outputCharFiveDisplay = BuildOutputDisplay(wordleWordCharFive, userGuessCharFive, outputDisplayCharFiveToString, outputCharFiveDisplay);

                            wordleBoard[useWordleBoardRow] = $"{outputCharOneDisplay} {outputCharTwoDisplay} {outputCharThreeDisplay} {outputCharFourDisplay} {outputCharFiveDisplay}";
                            useWordleBoardRow++;

                            foreach (string row in wordleBoard)
                            {
                                Console.WriteLine($"{row,28}");
                            }

                            Console.WriteLine("");
                            Console.WriteLine($"Letter Board: {letterBoardRow1}");
                            Console.WriteLine($" {letterBoardRow2,31}");
                            Console.WriteLine($" {letterBoardRow3,28}");
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
                    wordleBoard[useWordleBoardRow] = $"{outputCharOneDisplay} {outputCharTwoDisplay} {outputCharThreeDisplay} {outputCharFourDisplay} {outputCharFiveDisplay}";

                    foreach (string row in wordleBoard)
                    {
                        Console.WriteLine($"{row,28}");
                    }

                    Console.WriteLine("");
                    Console.WriteLine($"Letter Board: {letterBoardRow1}");
                    Console.WriteLine($" {letterBoardRow2,31}");
                    Console.WriteLine($" {letterBoardRow3,28}");
                    Console.WriteLine("");
                    Console.WriteLine($"\"You have 0 guesses remaining. The wordle was \"{wordleWord.ToUpper()}\".Thank you for playing. Good Bye!\"");
                    return;
                }
                else
                {
                    if ((userInputValidationPassed) && (wordleListValidationPassed))
                    {
                        Console.WriteLine($"Guess Attempt(s) Remaining: {guessAttemptsRemaining}");
                        wordleBoard[useWordleBoardRow] = $"{outputCharOneDisplay} {outputCharTwoDisplay} {outputCharThreeDisplay} {outputCharFourDisplay} {outputCharFiveDisplay}";
                        useWordleBoardRow++;

                        foreach (string row in wordleBoard)
                        {
                            Console.WriteLine($"{row,28}");
                        }

                        Console.WriteLine("");
                        Console.WriteLine($"Letter Board: {letterBoardRow1}");
                        Console.WriteLine($" {letterBoardRow2,31}");
                        Console.WriteLine($" {letterBoardRow3,28}");
                        Console.WriteLine("");
                    }
                    else
                    {
                        Console.WriteLine($"Guess Attempt(s) Remaining: {guessAttemptsRemaining}");
                        //wordleBoard[useWordleBoardRow] = $"{outputCharOneDisplay} {outputCharTwoDisplay} {outputCharThreeDisplay} {outputCharFourDisplay} {outputCharFiveDisplay}";

                        foreach (string row in wordleBoard)
                        {
                            Console.WriteLine($"{row,28}");
                        }

                        Console.WriteLine("");
                        Console.WriteLine($"Letter Board: {letterBoardRow1}");
                        Console.WriteLine($" {letterBoardRow2,31}");
                        Console.WriteLine($" {letterBoardRow3,28}");
                        Console.WriteLine("");
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

        public string letterBoard(string letterBoard, string incorrectLetterBoardCharOne, string incorrectLetterBoardCharTwo, string incorrectLetterBoardCharThree, string incorrectLetterBoardCharFour, string incorrectLetterBoardCharFive)
        {
            letterBoard = letterBoard.ToLower();
            letterBoard = letterBoard.Replace(System.Convert.ToChar(incorrectLetterBoardCharOne), ' ').Replace(System.Convert.ToChar(incorrectLetterBoardCharTwo), ' ').Replace(System.Convert.ToChar(incorrectLetterBoardCharThree), ' ').Replace(System.Convert.ToChar(incorrectLetterBoardCharFour), ' ').Replace(System.Convert.ToChar(incorrectLetterBoardCharFive), ' ');
            letterBoard = letterBoard.ToUpper();
            return letterBoard;
        }
    }
}