using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace textClassification
{
    class BOW
    {
        private static List<String> BagOfWords = new List<String>();
        private static dynamic path = Directory.EnumerateFiles(@"..\..\..\..\Articles\TrainingData", "*.txt");

        /// create bag of words
        public static List<String> CreateBoW()
        {

            int article = 1;

            foreach (string file in path)
            {

                var everyWords = System.Text.RegularExpressions.Regex.Split(File.ReadAllText(file), @"[\s,;:.!?-]+");

                //Creating new files with the cleaned words with SteamWriter
                TextWriter tw;
                if (file.Contains("politic"))
                {
                    tw = new StreamWriter(@"..\..\..\..\Articles\CleanedText\politic" + article.ToString() + ".txt");
                }
                else if (file.Contains("sport"))
                {
                    tw = new StreamWriter(@"..\..\..\..\Articles\CleanedText\sport" + article.ToString() + ".txt");
                }
                else
                {
                    tw = new StreamWriter(@"..\..\..\..\Articles\CleanedText\unkown" + article.ToString() + ".txt");
                }

                // Get all words from txt files
                foreach (var word in everyWords)
                {
                    var builder = new StringBuilder();
                    foreach (char letter in word)
                    {
                        // If the char matches basic latin alphabet (without special chars and punctuations) 
                        //than the program will add them to the StringBuilder
                        if (letter <= 90 && letter >= 65 || letter >= 97 && letter <= 122)
                        {
                            builder.Append(letter.ToString().ToLower());
                        }
                    }

                    // All words which are longer than 2 chars will be added to the list
                    if (builder.Length > 2)
                    {
                        tw.WriteLine(builder);

                        if (!BagOfWords.Contains(builder.ToString()))
                        {
                            BagOfWords.Add(builder.ToString());
                        }
                    }
                }
                tw.Close();
                article++;
            }
            return BagOfWords;
        }
    }
}
