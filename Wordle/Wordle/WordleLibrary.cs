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
            "pizza",
            "dizzy",
            "quick",
            "quack",
            "froze",
            "juice",
            "amaze",
            "check",
            "chick",
        };

        public static Dictionary<int, WordleClass> GetWordle()
        {
            var wordleDictionary = new Dictionary<int, WordleClass>();
            int indexNumber = 0;

            foreach (var item in wordleList)
            {
                wordleDictionary.Add(indexNumber, new WordleClass(item));
                indexNumber++;
            }
            return wordleDictionary;
        }
    }
}
