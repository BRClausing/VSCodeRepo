using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSolutions.InterviewQuestions
{
    internal class Sorting
    {
        /// <summary>
        /// GitHub Copilot generated QuickSort implementation
        /// Interesting that it uses LINQ and array slicing because that is essentially cheating, which is fine for an AI.
        /// </summary>
        /// <param name="listOfNumbers"></param>
        /// <returns></returns>
        public int[] QSortAI(int[] listOfNumbers)
        {
            if (listOfNumbers.Length <= 1)
            {
                return listOfNumbers;
            }
            int pivot = listOfNumbers[listOfNumbers.Length / 2];
            int[] left = [.. listOfNumbers.Where(x => x < pivot)];
            int[] middle = listOfNumbers.Where(x => x == pivot).ToArray();
            int[] right = listOfNumbers.Where(x => x > pivot).ToArray();
            return QSortAI(left).Concat(middle).Concat(QSortAI(right)).ToArray();
        }

        private static int Partition(int[] list, int lowIndex, int highIndex)
        {
            int pivot = list[highIndex];
            int i = (lowIndex - 1);
            for (int j = lowIndex; j < highIndex; j++)
            {
                if (list[j] <= pivot)
                {
                    i++;
                    (list[i], list[j]) = (list[j], list[i]);
                }
            }
            i++;
            (list[i], list[highIndex]) = (list[highIndex], list[i]);
            return i;
        }

        public static int[] MyQuickSort(int[] array, int lowIndex, int highIndex)
        {
            if (lowIndex < highIndex)
            {
                int pivotIndex = Partition(array, lowIndex, highIndex);
                MyQuickSort(array, lowIndex, pivotIndex - 1);
                MyQuickSort(array, pivotIndex + 1, highIndex);
            }
            return array;
        }
    }
}
