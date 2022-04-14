using System;
using System.Collections.Generic;
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

            Console.WriteLine("");
            Console.WriteLine(letterBoardRow1);
            Console.WriteLine("");
            Console.WriteLine("Enter 'Quit' to exit the game.");
            Console.WriteLine("");

            bool guessAgain = true;

            int guessAttemptsRemaining = 3;

            do
            {
                Console.Write("Guess here: ");
                string userGuess = Console.ReadLine().ToLower();

                int userGuessLength = userGuess.Length;

                int containInvalidCharCount = 0;

                bool userInputValidationPassed = true;

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

                    if (WordleClass.wordleList.Contains(userGuess))
                    {

                        if (userGuess != wordleWord)
                        {
                            guessAttemptsRemaining--;

                            outputCharOneDisplay = BuildOutputDisplay(wordleWordCharOne, userGuessCharOne, outputDisplayCharOneToString, outputCharOneDisplay);
                            outputCharTwoDisplay = BuildOutputDisplay(wordleWordCharTwo, userGuessCharTwo, outputDisplayCharTwoToString, outputCharTwoDisplay);
                            outputCharThreeDisplay = BuildOutputDisplay(wordleWordCharThree, userGuessCharThree, outputDisplayCharThreeToString, outputCharThreeDisplay);
                            outputCharFourDisplay = BuildOutputDisplay(wordleWordCharFour, userGuessCharFour, outputDisplayCharFourToString, outputCharFourDisplay);
                            outputCharFiveDisplay = BuildOutputDisplay(wordleWordCharFive, userGuessCharFive, outputDisplayCharFiveToString, outputCharFiveDisplay);

                            letterBoardRow1 = letterBoard(letterBoardRow1, outputCharOneDisplay, outputCharTwoDisplay, outputCharThreeDisplay, outputCharFourDisplay, outputCharFiveDisplay);
                            letterBoardRow2 = letterBoard(letterBoardRow2, outputCharOneDisplay, outputCharTwoDisplay, outputCharThreeDisplay, outputCharFourDisplay, outputCharFiveDisplay);
                            letterBoardRow3 = letterBoard(letterBoardRow3, outputCharOneDisplay, outputCharTwoDisplay, outputCharThreeDisplay, outputCharFourDisplay, outputCharFiveDisplay);
                        }
                        else
                        {
                            outputCharOneDisplay = BuildOutputDisplay(wordleWordCharOne, userGuessCharOne, outputDisplayCharOneToString, outputCharOneDisplay);
                            outputCharTwoDisplay = BuildOutputDisplay(wordleWordCharTwo, userGuessCharTwo, outputDisplayCharTwoToString, outputCharTwoDisplay);
                            outputCharThreeDisplay = BuildOutputDisplay(wordleWordCharThree, userGuessCharThree, outputDisplayCharThreeToString, outputCharThreeDisplay);
                            outputCharFourDisplay = BuildOutputDisplay(wordleWordCharFour, userGuessCharFour, outputDisplayCharFourToString, outputCharFourDisplay);
                            outputCharFiveDisplay = BuildOutputDisplay(wordleWordCharFive, userGuessCharFive, outputDisplayCharFiveToString, outputCharFiveDisplay);

                            Console.WriteLine($"Progress: {outputCharOneDisplay} {outputCharTwoDisplay} {outputCharThreeDisplay} {outputCharFourDisplay} {outputCharFiveDisplay}");
                            Console.WriteLine("\"You have guessed the Durrrdle. Thank you for playing!\"");
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
                    Console.WriteLine("\"You have 0 guesses remaining. Thank you for playing. Good Bye!\"");
                    return;
                }
                else
                {
                    Console.WriteLine($"Guess Attempt(s) Remaining: {guessAttemptsRemaining}");
                    Console.WriteLine($"Progress: {outputCharOneDisplay} {outputCharTwoDisplay} {outputCharThreeDisplay} {outputCharFourDisplay} {outputCharFiveDisplay}");
                    Console.WriteLine($"Letter Board: {letterBoardRow1}");
                    Console.WriteLine($" {letterBoardRow2, 31}");
                    Console.WriteLine($" {letterBoardRow3, 28}");
                    Console.WriteLine("");
                }

            } while (guessAgain);
        }
        public string BuildOutputDisplay(char durrrdleWordChar, char userGuessChar, string outputDisplayChar, string finalOutputDisplayChar)
        {
            if ((outputDisplayChar == "_") && (durrrdleWordChar == userGuessChar))
            {
                finalOutputDisplayChar = userGuessChar.ToString();
            }
            else
            {
                finalOutputDisplayChar = outputDisplayChar;
            }
            return finalOutputDisplayChar;
        }

        public string letterBoard(string letterBoard, string outputCharOneDisplay, string outputCharTwoDisplay, string outputCharThreeDisplay, string outputCharFourDisplay, string outputCharFiveDisplay)
        {
            letterBoard = letterBoard.ToLower();
            letterBoard = letterBoard.Replace(System.Convert.ToChar(outputCharOneDisplay), '_').Replace(System.Convert.ToChar(outputCharTwoDisplay), '_').Replace(System.Convert.ToChar(outputCharThreeDisplay), '_').Replace(System.Convert.ToChar(outputCharFourDisplay), '_').Replace(System.Convert.ToChar(outputCharFiveDisplay), '_');
            letterBoard = letterBoard.ToUpper();
            return letterBoard;
        }
    }
}