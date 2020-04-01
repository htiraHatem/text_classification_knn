using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace textClassification
{
    class Vector
    {
        public String label { get; set; }
        public int[] vector { get; set; }
      
        private static dynamic cleanedFiles;
        private static int[] inputVector;
        private static String[] wordsInInput = System.Text.RegularExpressions.Regex.Split(File.ReadAllText(@"..\..\..\..\Articles\Input\Input.txt"), @"[\s,;:.!?-]+");

        public Vector(string label, int[] vector)
        {
            this.label = label;
            this.vector = vector;
        }
        public static void GetWordsFromInput()
        {
            List<String> listOfInputWords = new List<String>();

            foreach (var word in wordsInInput)
            {

                var builder = new StringBuilder();

                foreach (char letter in word)
                {

                    if (letter <= 90 && letter >= 69 || letter >= 97 && letter <= 122)
                    {
                        builder.Append(letter);
                    }

                    listOfInputWords.Add(builder.ToString().ToLower());
                }
            }
        }
        public static int[] generateInputVector(List<String> BagOfWords)
        {

            inputVector = new int[BagOfWords.Count];

            for (int i = 0; i < BagOfWords.Count; i++)
            {
                if (wordsInInput.Contains(BagOfWords[i]))
                {
                    inputVector[i] = 1;
                }
                else
                {
                    inputVector[i] = 0;
                }
            }
            return inputVector;

        }
        public static List<Vector> CreateVector(List<String> BagOfWords)
        {
            List<Vector> listOfVectors = new List<Vector>();
            cleanedFiles = Directory.EnumerateFiles(@"..\..\..\..\Articles\CleanedText\", "*.txt");

            foreach (var cleanedFile in cleanedFiles)
            {
                // Load all words in that file into a string array.
                String[] Words = System.Text.RegularExpressions.Regex.Split(File.ReadAllText(cleanedFile), @"[\s,;:.!?-]+");
                String label = "";
                int[] temporaryVectorList = new int[BagOfWords.Count];

                // For every word in BagOfWords in the cleaned Textfile. check if the WORD is present in the file, return value 1, else 0. 
                for (int i = 0; i < BagOfWords.Count; i++)
                {
                    if (Words.Contains(BagOfWords[i]))
                    {
                        temporaryVectorList[i] = 1;
                    }
                    else
                    {
                        temporaryVectorList[i] = 0;
                    }
                }

                // Based on the file name of the cleaned textfile, identify which theme it belongs to and save that as the attached label.
                if (cleanedFile.Contains("politic"))
                {
                    label = "politic";
                }
                else if (cleanedFile.Contains("sport"))
                {
                    label = "sport";
                }

                // Add a new entry into my listOfVector with the Object Vector, which consists of a label to indentify it's belonging, along with an int vector of 0,0,0,0,1,1,1 etc.
                listOfVectors.Add(new Vector(label, temporaryVectorList));
            }
            return listOfVectors;
        }
    }

}
