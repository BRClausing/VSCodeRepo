using System;
using System.Collections.Generic;

namespace LeetCode
{
    public class FirstDupLetter
    {
        readonly string _input;
        readonly Dictionary<char, int> _letterCount;

        public FirstDupLetter(string input)
        {
            _input = input;
            _letterCount = new Dictionary<char, int>();
        }

        public char FindFirstDupLetter()
        {
            foreach (char letter in _input)
            {
                if (_letterCount.ContainsKey(letter))
                {
                    return letter;
                }
                else
                {
                    _letterCount.Add(letter, 1);
                }
            }
            return ' ';
        }
    }
}