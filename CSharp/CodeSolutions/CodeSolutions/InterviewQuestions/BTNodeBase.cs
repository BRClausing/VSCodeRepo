using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewQuestions
{
    public class BTNode
    {
        public BTNode LeftNode { get; set; }
        public BTNode RightNode { get; set; }
        public string NodeValue { get; set; }

        public BTNode()
            : this(null, "0", null)
        { }
        
        public BTNode(int nodeValue)
            : this(null, nodeValue.ToString(), null)
        { }

        public BTNode(BTNode leftNode, string nodeValue, BTNode rightNode)
        {
            LeftNode = leftNode;
            RightNode = rightNode;
            NodeValue = nodeValue;
        }
    }
}
