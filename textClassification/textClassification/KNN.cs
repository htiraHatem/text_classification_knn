using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace textClassification
{
    class KNN
    {
        // The kValue decides how many neighbours we should compare our text to. We want as many as makes sense. 
        private static int kValue = 3;


        // Calculate the distance between vectors and based on that the application will detect the category of the article
        public static void kNNCalculationsAndResult(List<Vector> listOfVectors ,int[] inputVector)
        {
            //a list initialized which will contain all distances along with their labels
            List<Result> vectorResults = new List<Result>();

            for (int l = 0; l < listOfVectors.Count; l++)
            {
                double denominator = 0;

                // Calculate the difference between index[i] in the input vector with the index[i] of first testvector
                for (int i = 0; i < inputVector.Length; i++)
                {
                    denominator += Math.Pow(inputVector[i] - (int)listOfVectors.ElementAt(l).vector.GetValue(i), 2);
                }
                // Based on the total sum above, get the distance by doing a square root calculation 
                double distance = Math.Sqrt(denominator);
                vectorResults.Add(new Result(listOfVectors.ElementAt(l).label, distance));
            }

            //sort the distances from lowest to highest and save the result in a new list
            List<Result> orderedResultList = vectorResults.OrderBy(o => o.result).ToList();

            //print our all distances + their labels.
            Console.WriteLine("order by distance : ");
            Console.WriteLine("with k  = "+ kValue);
            Console.WriteLine("order / Distance / Class");
            Console.WriteLine("==========================");
            int order = 0;
;            foreach (var result in orderedResultList)
            {
                Console.WriteLine( "  " + ++order +"  :   " + string.Format("{0:0.000}", result.result) + "   :  " + result.label.ToString());
            }

            int counterPolitic = 0;
            int counterSport = 0;

            for (int i = 0; i < kValue; i++)
            {
                if (orderedResultList.ElementAt(i).label.ToString().Equals("politic"))
                {
                    counterPolitic++;
                }
                else if (orderedResultList.ElementAt(i).label.ToString().Equals("sport"))
                {
                    counterSport++;
                }
            }
            // check if the input text whether is a politic or a sport article
            if (counterPolitic > counterSport)
            {
                Console.WriteLine("Your text is recognized as a politic article");
            }
            else if (counterPolitic < counterSport)
            {
                Console.WriteLine("Your text is recognized as a Sport article");
            }
            else if (counterPolitic == counterSport)
            {
                Console.WriteLine("With the current k value we can't determine what the article resembles the most");
            }
        }
    }
}
