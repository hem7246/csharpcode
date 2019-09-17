using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace C_Sharp_rocks
{

    public enum OperaionType
    {
        Delete,
        Insert,
        Swap,
        Repace
    }
    public static class Program
    {
        static List<string> word12 = new List<string>();

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
            char[] charArray = word.ToCharArray();
            Array.Reverse(charArray);

            String s2 = new string(charArray);
            return s2 == word;
            //  return false;
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
            String pattern = @"(\S*)@\S*\.\S*([a-z]|[A-Z]|[0-9])";
            return text = Regex.Replace(text, pattern, replacement);

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


        static int GetAlternativeWordsCount(int wordLength, int alphabetLength)
        {
            // TODO: 3.b Write a method that can calculate the number of non-unique alternative words based on input word length and alphabet length (assuming maximum Damerau–Levenshtein distance = 1).

            return 0;




        }



        public static int EditDistance(string original, string modified)
        {
            int len_orig = original.Length;
            int len_diff = modified.Length;

            var matrix = new int[len_orig + 1, len_diff + 1];
            for (int i = 0; i <= len_orig; i++)
                matrix[i, 0] = i;
            for (int j = 0; j <= len_diff; j++)
                matrix[0, j] = j;

            for (int i = 1; i <= len_orig; i++)
            {
                for (int j = 1; j <= len_diff; j++)
                {
                    int cost = modified[j - 1] == original[i - 1] ? 0 : 1;
                    var vals = new int[] {
                        matrix[i - 1, j] + 1,
                        matrix[i, j - 1] + 1,
                        matrix[i - 1, j - 1] + cost
                    };
                    matrix[i, j] = vals.Min();
                    if (i > 1 && j > 1 && original[i - 1] == modified[j - 2] && original[i - 2] == modified[j - 1])
                        matrix[i, j] = Math.Min(matrix[i, j], matrix[i - 2, j - 2] + cost);
                }
            }
            return matrix[len_orig, len_diff];
        }



        private static List<string> permute(String str,
                               int l, int r)
        {

            if (l == r)
                word12.Add(str);
            else
            {
                for (int i = l; i <= r; i++)
                {
                    str = swap(str, l, i);
                    permute(str, l + 1, r);
                    str = swap(str, l, i);
                }
            }
            return word12;
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

