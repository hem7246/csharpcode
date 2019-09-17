using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_Sharp_rocks
{
    class Program
    {
        static void Main(string[] args)
        {
            string word = "test";
            string text = "Christian has the email address christian+123@gmail.com. " +
                "Christian's friend, Lars-Ole Jensen, has the email address lars-ole.jensen@gmail.com. " +
                "Lars-Ole's daugther Britt studies at Oxford University and has the email adress britt123@oxford.co.uk.";

            Console.WriteLine(string.Format("IsPalindrome(\"{0}\"): {1}", word, IsPalindrome(word)));
            Console.WriteLine();

            Console.WriteLine("Text with valid emails replaced by \"[EMAIL]\":");
            Console.WriteLine(ReplaceEmails(text, "[EMAIL]"));
            Console.WriteLine();

            List<string> alternativeWords = GenerateWords(word);
            Console.WriteLine("Alternative words:");
            Console.WriteLine(string.Join(", ", alternativeWords));
            Console.WriteLine();

            // TODO: 3.a How many non-unique alternative words can be generated using the word test, alphabet a-z (26 letters) and maximum Damerau–Levenshtein distance = 1?
            Console.WriteLine(string.Format("Number of alternative words: {0}", alternativeWords.Count));
            Console.WriteLine();

            Console.WriteLine(string.Format("Possible non-unique alternatives using distance = 1 for \"{0}\": {1}", word, GetAlternativeWordsCount(word.Length, 26)));
            Console.WriteLine();

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

        static bool IsPalindrome(string word)
        {
            // TODO: 1. Write a function that performs a web request to fetch word predictions.

            return false;
        }

        /// <summary>
        /// TODO: 2.a In the summary of the method explain your considerations about the regex. 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="replacement"></param>
        /// <returns></returns>
        static string ReplaceEmails(string text, string replacement)
        {
            // TODO: 2. Write a method that can find and replace valid email adresses in a (string) using a regex.

            return text;
        }

        static List<string> GenerateWords(string word)
        {
            // TODO: 3.Write a method that can generate a list of words based on input word and alphabet.
            // Alphabet can be provided as a parameter, generated internally or whatever you find appropiate

            return new List<string>();
        }

        static int GetAlternativeWordsCount(int wordLength, int alphabetLength)
        {
            // TODO: 3.b Write a method that can calculate the number of non-unique alternative words based on input word length and alphabet length (assuming maximum Damerau–Levenshtein distance = 1).

            return 0;
        }
    }
}
