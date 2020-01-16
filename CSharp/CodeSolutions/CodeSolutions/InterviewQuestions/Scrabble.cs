using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewQuestions
{
    class Scrabble
    {
        // return the first element of a list
        private object car(IList<object> list)
        {
            if (list != null && list.Count() > 0)
                return list[0];
            else
                return null;
        }

        // return the 'rest' of the list, i.e. (the whole list) - (the fisrt element)
        private IList<object> cdr(IList<object> list)
        {
            list.RemoveAt(0);
            return list;
        }

        // prepend an element onto the list
        private void cons(object item, IList<object> list)
        {
            list.Insert(0, item);
        }

        // List predicates
        // Is type == Int32
        private bool isNumber(object obj)
        {
            if (obj != null)
                return (obj.GetType() == typeof(int));
            else
                return false;
        }

        // Is type == IList<object>
        private bool isList(object obj)
        {
            if (obj != null)
                return (obj.GetType() == typeof(List<object>));
            else
                return false;
        }

        private IList<object> _theBoard;

        public Scrabble()
        {
            _theBoard = new List<object>();

            // Build default
            //  Sample board:
            //          1 2 3 7
            //              4
            //              5 8 9 10
            //              6
            List<object> myWordDown = new List<object>();
            List<object> myWordAcross = new List<object>();
            myWordAcross.Add(5);
            myWordAcross.Add(8);
            myWordAcross.Add(9);
            myWordAcross.Add(10);

            myWordDown.Add(3);
            myWordDown.Add(4);
            myWordDown.Add(myWordAcross);
            myWordDown.Add(6);

            _theBoard.Add(1);
            _theBoard.Add(2);
            _theBoard.Add(myWordDown);
            _theBoard.Add(7);
        }

        public Scrabble(IList<object> initialBoard)
        {
            _theBoard = initialBoard;
        }

        public int CountPoints()
        {
            return BoardTotal(_theBoard);
        }

        private int BoardTotal(IList<object> list)
        {
            int boardCount = 0;

            if (list != null)
            {
                object item = this.car(list); // get first item

                if (isList(item))
                {
                    boardCount = BoardTotal((IList<object>)item) + BoardTotal((IList<object>)this.cdr(list));
                }
                else if (isNumber(item))
                {
                    boardCount = (int)item + BoardTotal((IList<object>)this.cdr(list));
                }
            }

            return boardCount;
        }
    }
}
