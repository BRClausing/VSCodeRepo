using System;
using System.Collections.Generic;

namespace LeetCode
{
    /*************************************************************
        Given an array nums of n integers, are there elements a, b, c in nums such that a + b + c = 0? Find all unique triplets in the array which gives the sum of zero.

        Note:
            The solution set must not contain duplicate triplets.

        Example:
            Given array nums = [-1, 0, 1, 2, -1, -4],

        A solution set is:
            [
              [-1, 0, 1],
              [-1, -1, 2]
            ]
    *************************************************************/
    //public class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        int[] data = new int[6] { -1, 0, 1, 2, -1, -4 };
    //        ThreeSumSolution solution = new ThreeSumSolution(data);
    //        solution.PrintToConsole();
    //    }
    //}
    public class ThreeSumSolution
    {
        readonly int[] _nums;
        readonly List<List<int>> _threeSumTriples;

        public ThreeSumSolution(int[] data)
        {
            _nums = data;
            _threeSumTriples = Dedup(ValidSolutions(new List<List<int>>()));
        }
        public List<List<int>> ValidSolutions(List<List<int>> threeSumTriples)
        {
            for (int i = 0; i < _nums.Length; i++)
            {
                int key = i;
                for (int j = 0; j < (_nums.Length - 1); j++)
                {
                    if (j != key)
                    {
                        // Add a triple
                        if ((j + 1 == key) && (j + 2 < _nums.Length))
                        {
                            AddTriple(threeSumTriples, _nums[key], _nums[j], _nums[j + 2]);
                        }
                        else
                        {
                            AddTriple(threeSumTriples, _nums[key], _nums[j], _nums[j + 1]);
                        }
                    }
                }
            }

            return threeSumTriples;
        }

        public void AddTriple(List<List<int>> all, int first, int second, int third)
        {
            if (first + second + third == 0)
            {
                // Sort items to enable a de-dup check later
                if (first > second)
                {
                    int temp = first;
                    first = second;
                    second = temp;
                }
                if (second > third)
                {
                    int temp = second;
                    second = third;
                    third = temp;
                }
                all.Add(new List<int>() { first, second, third });
            }
        }

        public List<List<int>> Dedup(List<List<int>> threeSumTriples)
        {
            List<List<int>> noDups = new List<List<int>>();
            foreach (List<int> triple in threeSumTriples)
            {
                bool dupFound = false;
                foreach (List<int> noDupTriple in noDups)
                {
                    if (triple[0] == noDupTriple[0] &&
                        triple[1] == noDupTriple[1] &&
                        triple[2] == noDupTriple[2])
                    {
                        dupFound = true;
                        break;
                    }
                }

                if (!dupFound)
                {
                    noDups.Add(triple);
                }
            }

            return noDups;
        }
        
        public void PrintToConsole()
        {
            foreach (List<int> triple in _threeSumTriples)
            {
                Console.WriteLine($"[{triple[0]},{triple[1]},{triple[2]}]");
            }
        }
    }
}

