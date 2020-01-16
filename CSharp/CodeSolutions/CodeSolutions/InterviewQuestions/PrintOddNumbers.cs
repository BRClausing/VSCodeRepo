using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewQuestions
{
    public class PrintOddNumbers
    {
        public void Execute()
        {
            foreach (int i in Enumerable.Range(1, 99))
            {
                if ((i & 1) == 1)
                {
                    Console.WriteLine(i);
                }
            }

            Console.ReadKey();
        }

        public void Execute2()
        {
            foreach (int i in Enumerable.Range(1, 99).Where(num => (num & 1) == 1))
            {
                Console.WriteLine(i);
            }

            Console.ReadKey();
        }
    }
}
