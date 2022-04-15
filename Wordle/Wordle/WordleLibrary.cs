using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Wordle
{
    public class WordleClass
    {
        public string Word { get; set; }

        public WordleClass(string wordleWord)
        {
            Word = wordleWord;
        }

        private const string wordle_Library_File_Path = @"C:\Users\benja\Documents\Projects\Wordle\Wordle\Wordle\WordleLibrary.txt";
        public static List<string> wordleList = File.ReadAllLines(wordle_Library_File_Path).ToList();

        public static Dictionary<int, WordleClass> GetWordle()
        {
            var wordleDictionary = new Dictionary<int, WordleClass>();
            int indexNumber = 0;

            foreach (string item in wordleList)
            {
                wordleDictionary.Add(indexNumber, new WordleClass(item));
                indexNumber++;
            }
            return wordleDictionary;
        }
    }
}
