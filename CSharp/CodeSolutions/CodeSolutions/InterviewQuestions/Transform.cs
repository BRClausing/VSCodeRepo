using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewQuestions
{
    // Question description
    // The first half of the question, which some people (whose names I will protect to my dying breath, but their initials are Willie Lewis) feel is a 
    // Job Requirement If You Want To Call Yourself A Developer And Work At Amazon, is actually kinda hard. The question is: how do you go from an 
    // arithmetic expression (e.g. in a string) such as "2 + (2)" to an expression tree. We may have an ADJ challenge on this question at some point.
    //
    // The second half is: let's say this is a 2-person project, and your partner, who we'll call "Willie", is responsible for transforming the string 
    // expression into a tree. You get the easy part: you need to decide what classes Willie is to construct the tree with. You can do it in any language, 
    // but make sure you pick one, or Willie will hand you assembly language. If he's feeling ornery, it will be for a processor that is no longer 
    // manufactured in production. 
    //
    // Example tree:  "[(2-1)+3]-[(4/2)+1]" yields:  
    //
    //                        minus
    //                       /    \
    //                     plus   plus
    //                    /  \   /  \
    //                  minus 3 div  1
    //                  / \     / \
    //                 2   1   4   2
                           

    /// <summary>
    /// Transform a string arithmetic expression into a Binary Tree
    /// </summary>
    class Transform
    {
        public static BRC_BinaryTree StringToBT(string expression)
        {
            return new BRC_BinaryTree(expression);
        }
    }
}
