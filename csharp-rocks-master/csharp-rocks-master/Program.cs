using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;

namespace CodeDemo
{
    static class Program
    {

        static List<string> ListGenrated = new List<string>();
        static void Main(string[] args)
        {
            string word = "test";
            string text = "Christian has the email address christian+123@gmail.com. " +
                "Christian's friend, Lars-Ole Jensen, has the email address lars-ole.jensen@gmail.com. " +
                "Lars-Ole's daugther Britt studies at Oxford University and has the email adress britt123@oxford.co.uk.";

            Console.WriteLine(string.Format("IsPalindrome(\"{0}\"): {1}", word, IsPalindrome(word)));
            Console.WriteLine();

            Console.WriteLine("Prints the numbers from 1 to 100, but for multiples of 3 print Foo, for multiples of 5 print Bar and for numbers that are multiples of both 3 and 5 print FooBar.:");
            //Added 2 parameters for flexible code.
            PrintFoorBar(1, 100);
            Console.WriteLine();

            Console.WriteLine("Text with valid emails replaced by \"[EMAIL]\":");
            Console.WriteLine(ReplaceEmails(text, "[EMAIL]"));
            Console.WriteLine();


            List<string> strDatabase = GenerateWords(word).ToList();

            List<string> alternativeWords = GenerateWords("test", strDatabase);
            Console.WriteLine("Alternative words:");
            Console.WriteLine(string.Join(", ", alternativeWords));
            Console.WriteLine();

            // TODO: 4.a How many non-unique alternative words can be generated using the word test, alphabet a-z (26 letters) and maximum Damerau–Levenshtein distance = 1?
            Console.WriteLine(string.Format("Number of alternative words: {0}", alternativeWords.Count));
            Console.WriteLine();

            Console.WriteLine(string.Format("Possible non-unique alternatives using distance = 1 for \"{0}\": {1}", word, GetAlternativeWordsCount(word.Length, 26)));
            Console.WriteLine();

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

        static bool IsPalindrome(string word)
        {

            string str1 = word.Substring(0, word.Length / 2);

            char[] chrArr = word.ToCharArray();

            Array.Reverse(chrArr);
            string str2 = new string(chrArr).Substring(0, word.Length / 2);

            return str1.Equals(str2);
        }

        static void PrintFoorBar(int firstNumber, int lastNumber)
        {
            // TODO: 2. Write a method that prints the numbers from 1 to 100, but for multiples of 3 print Foo,
            // for multiples of 5 print Bar and for numbers that are multiples of both 3 and 5 print FooBar.
            int numCounter = firstNumber;
            while (numCounter <= lastNumber)
            {
                Console.WriteLine((numCounter % 3 == 0) && (numCounter % 5 == 0) ? "FooBar"
                             : (numCounter % 3 == 0) ? "Foo"
                             : (numCounter % 5 == 0) ? "Bar"
                             : numCounter.ToString());


                numCounter += 1;
            }
        }

        /// <summary>
        /// TODO: 3.a In the summary of the method explain your considerations about the regex. 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="replacement"></param>
        /// <returns></returns>
        static string ReplaceEmails(string text, string replacement)
        {
            String pattern = @"(\S*)@\S*\.\S*([a-z]|[A-Z]|[0-9])";
            return text = Regex.Replace(text, pattern, replacement);

        }



        static List<string> GenerateWords(string word, List<string> wordDatabase)
        {

            List<string> resultWords = new List<string>();

            foreach (string str in wordDatabase)
            {
                if (levenshtein(word, str) == 1)
                    resultWords.Add(str);
            }

            return resultWords;
            // TODO: 4.Write a method that can generate a list of words based on input word and alphabet.
            // Alphabet can be provided as a parameter, generated internally or whatever you find appropiate

            //return new List<string>();
        }

        static int GetAlternativeWordsCount(int wordLength, int alphabetLength)
        {
            // TODO: 4.b Write a method that can calculate the number of non-unique alternative words based on input word length and alphabet length (assuming maximum Damerau–Levenshtein distance = 1).

            return LevenshteinDistance(wordLength, alphabetLength);
        }



        static int levenshtein(string a, string b)
        {

            if (string.IsNullOrEmpty(a))
            {
                if (!string.IsNullOrEmpty(b))
                {
                    return b.Length;
                }
                return 0;
            }

            if (string.IsNullOrEmpty(b))
            {
                if (!string.IsNullOrEmpty(a))
                {
                    return a.Length;
                }
                return 0;
            }

            int cost;
            int[,] d = new int[a.Length + 1, b.Length + 1];
            int min1;
            int min2;
            int min3;

            for (int i = 0; i <= d.GetUpperBound(0); i += 1)
            {
                d[i, 0] = i;
            }

            for (int i = 0; i <= d.GetUpperBound(1); i += 1)
            {
                d[0, i] = i;
            }

            for (int i = 1; i <= d.GetUpperBound(0); i += 1)
            {
                for (int j = 1; j <= d.GetUpperBound(1); j += 1)
                {
                    cost = (a[i - 1] != b[j - 1]) ? 1 : 0;

                    min1 = d[i - 1, j] + 1;
                    min2 = d[i, j - 1] + 1;
                    min3 = d[i - 1, j - 1] + cost;
                    d[i, j] = Math.Min(Math.Min(min1, min2), min3);
                }
            }

            return d[d.GetUpperBound(0), d.GetUpperBound(1)];

        }




        static int LevenshteinDistance(int source, int target)
        {
            if (source == 0)
            {
                if (target == 0) return 0;
                return target;
            }
            if (target == 0) return source;

            if (source > target)
            {
                var temp = target;
                target = source;
                source = temp;
            }

            var m = target;
            var n = source;
            var distance = new int[2, m + 1];
            // Initialize the distance matrix
            for (var j = 1; j <= m; j++) distance[0, j] = j;

            var currentRow = 0;
            for (var i = 1; i <= n; ++i)
            {
                currentRow = i & 1;
                distance[currentRow, 0] = i;
                var previousRow = currentRow ^ 1;
                for (var j = 1; j <= m; j++)
                {
                    var cost = (target == source ? 0 : 1);
                    distance[currentRow, j] = Math.Min(Math.Min(
                                distance[previousRow, j] + 1,
                                distance[currentRow, j - 1] + 1),
                                distance[previousRow, j - 1] + cost);
                }
            }
            return distance[currentRow, m];
        }

        static List<string> GenerateWords(string word)
        {
            // TODO: 3.Write a method that can generate a list of words based on input word and alphabet.
            // Alphabet can be provided as a parameter, generated internally or whatever you find appropiate

            List<string> values = new List<string>();

            // Build one-letter combinations.

            char[] p = word.ToCharArray();
            foreach (char str in p)
            {
                values.Add(str.ToString());
            }

            List<string> new_values = new List<string>();

            foreach (string str in values)
            {

                new_values.AddRange(getvalues("Delete", word, str));
                new_values.AddRange(getvalues("Insert", word, str));
                new_values.AddRange(getvalues("replace", word, str));

            }

            // Replace the old values with the new ones.
            new_values.AddRange(getvalues("swap", word, ""));



            values = new_values.Distinct().ToList();

            return values;
        }
        public static IEnumerable<int> GetAllIndexes(this string source, string matchString)
        {
            matchString = Regex.Escape(matchString);
            foreach (Match match in Regex.Matches(source, matchString))
            {
                yield return match.Index;
            }
        }


        static List<string> getvalues(string operation, string word, string charfirst)
        {
            List<string> new_values = new List<string>();



            IEnumerable<int> indexlist = GetAllIndexes(word, charfirst);

            if ("Delete" == operation)
            {

                foreach (var item in indexlist)
                {

                    int values = item;
                    //  int word1 = word.IndexOf(values);
                    string word1 = word.Remove(values, 1);
                    new_values.Add(word1);
                }

                return new_values;
            }


            switch (operation)
            {

                case "Insert":

                    foreach (var item in indexlist)
                    {
                        for (char ch = 'a'; ch <= 'z'; ch++)
                        {
                            int values = item;
                            //  int word1 = word.IndexOf(values);
                            string word1 = word.Insert(values, ch.ToString());
                            new_values.Add(word1);
                        }
                    }


                    new_values.Add(word);
                    break;

                case "replace":
                    foreach (var item in indexlist)
                    {



                        for (char ch = 'a'; ch <= 'z'; ch++)
                        {


                            int values = item;
                            char[] ch22 = word.ToCharArray();
                            ch22[values] = ch; // index starts at 0!
                            string newstring = new string(ch22);


                            new_values.Add(newstring);
                        }

                    }

                    break;
                case "swap":


                    int n = word.Length;
                    new_values.AddRange(permute(word, 0, n - 1));


                    break;
                default:
                    break;
            }


            return new_values;
        }
        private static List<string> permute(String str,
                              int l, int r)
        {

            if (l == r)
                ListGenrated.Add(str);
            else
            {
                for (int i = l; i <= r; i++)
                {
                    str = swap(str, l, i);
                    permute(str, l + 1, r);
                    str = swap(str, l, i);
                }
            }
            return ListGenrated;
        }

        /** 
        * Swap Characters at position 
        * @param a string value 
        * @param i position 1 
        * @param j position 2 
        * @return swapped string 
        */
        public static String swap(String a,
                                  int i, int j)
        {
            char temp;
            char[] charArray = a.ToCharArray();
            temp = charArray[i];
            charArray[i] = charArray[j];
            charArray[j] = temp;
            string s = new string(charArray);
            return s;
        }


    }
}
