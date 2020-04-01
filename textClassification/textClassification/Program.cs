using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace textClassification
{
    class Program
    {
        static void Main(string[] args)
        {
            List<String> BagOfWords= BOW.CreateBoW();
            Vector.GetWordsFromInput();
            KNN.kNNCalculationsAndResult(Vector.CreateVector(BagOfWords), Vector.generateInputVector(BagOfWords));
            System.Console.ReadKey();

        }
    }
}
