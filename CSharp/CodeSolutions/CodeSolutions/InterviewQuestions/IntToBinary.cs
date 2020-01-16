using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewQuestions
{
    class IntToBinary
    {
        public static void Print(int toPrint)
        {
            int counter = sizeof(int) * 16;
            Stack<int> bits = new Stack<int>();

            while (counter > 0)
            {
                if ((toPrint & 1) == 1)
                    bits.Push(1);
                else
                    bits.Push(0);

                toPrint = toPrint >> 1;
                counter--;
            }

            int totalCount = bits.Count();
            while (totalCount > 0)
            {
                if (totalCount % 16 == 0)
                    Console.Write(" ");

                Console.Write(bits.Pop().ToString());
                totalCount--;
            }

            Console.ReadKey();
        }
    }
}
