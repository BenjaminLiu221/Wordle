using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wordle
{
    public class WordleClass
    {
        public string Word { get; set; }

        public WordleClass(string wordleWord)
        {
            Word = wordleWord;
        }

        public static List<string> wordleList = new List<string>
        {
            "rhino",
            "block"
        };

        public static Dictionary<int, WordleClass> GetWordle()
        {
            var wordleLibrary = new Dictionary<int, WordleClass>();
            int indexNumber = 0;

            foreach (var item in wordleList)
            {
                wordleLibrary.Add(indexNumber, new WordleClass("rhino"));
                indexNumber++;
            }
            return wordleLibrary;
        }
    }
}
