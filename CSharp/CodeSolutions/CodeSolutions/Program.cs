using System;
using InterviewQuestions;
using LeetCode;

namespace CodeSolutions
{
    class Program
    {
        static void Main(string[] args)
        {
            // Question 1 - see Transform.cs for details
            //BRC_BinaryTree bt = Transform.StringToBT("[(2-1)+3]+2-(4/2)");
            //int calcResults = bt.EvaluateTree();

            // Question 2 - print all odd numbers up to 99
            //PrintOddNumbers pon = new PrintOddNumbers();
            //pon.Execute();
            //pon.AllInOne();

            // Question 3 - Provide the total count for all tiles on a scrabble board.
            //  Given:  The tiles only contain numbers (no letters to consider)
            //          The words can only go down and right from another word
            //
            //  Sample board:
            //          1 2 3 7
            //              4
            //              5 8 9 10
            //              6
            //  Sample result: 55
            //Scrabble myS = new Scrabble();
            //int totalCount = myS.CountPoints();

            //Console.WriteLine(totalCount);
            //Console.ReadKey();

            // Question 4 - print an int in binary
            //IntToBinary.Print(128);
            //Console.WriteLine(DecimalToBinary.ToExactString(33.33));
            //Console.ReadKey();

            // LeetCode
            //  ThreeSum solution
            int[] data = new int[6] { -1, 0, 1, 2, -1, -4 };
            ThreeSumSolution solution = new ThreeSumSolution(data);
            solution.PrintToConsole();
        }
    }
}
