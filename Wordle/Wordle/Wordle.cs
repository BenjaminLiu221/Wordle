using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wordle
{
    class Wordle
    {
        public void PlayGame()
        {
            Random randomizer = new Random();

            int randomNumber = (int)randomizer.Next(0, 10);

            var wordleLibrary = WordleClass.GetWordle();
            string wordleWord = wordleLibrary[randomNumber].Word;
            Console.WriteLine(wordleWord);
        }
    }
}
