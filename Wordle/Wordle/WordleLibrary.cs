using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wordle
{
    public class WordleWord
    {
        public string Word { get; set; }

        public WordleWord(string wordleWord)
        {
            Word = wordleWord;
        }

        public static Dictionary<int, WordleWord> GetWordle()
        {
            var wordleLibrary = new Dictionary<int, WordleWord>();
            var newWordle = new WordleWord("rhino");
            wordleLibrary.Add(0, newWordle);
            return wordleLibrary;
        }
    }
}
