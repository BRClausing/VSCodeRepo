using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewQuestions
{
    public enum BT_Operator
    {
        Add,
        Subtract,
        Multiply,
        Divide
    }

    public class BRC_BinaryTree
    {
        private BTNode _topNode;

        public BRC_BinaryTree(string arithmeticExpression)
        {
            this.BuildTree(arithmeticExpression);
        }
        
        private void BuildTree(string arithmeticExpression)
        {
            if (arithmeticExpression.Length >= 3)
            {
                _topNode = this.ParseExpression(arithmeticExpression);
            }
            else
            {
                throw new Exception("Invalid Arithmetic Expression provided");
            }
        }

        private BTNode ParseExpression(string arithmeticExpression)
        {
            BTNode newNode = null;

            string normilizedExpression = this.NormilizeExpression(arithmeticExpression);

            if (normilizedExpression.Length == 1)
            {
                // End case = leaf node; this is a number, not an operator
                int convertedValue;
                if (Int32.TryParse(normilizedExpression, out convertedValue))
                {
                    newNode = new BTNode(convertedValue);
                }
            }
            else
            {
                // Find operator
                int operatorIndex = FindOperatorIndex(normilizedExpression);

                // Store the operator
                char operatorValue = normilizedExpression[operatorIndex];

                // Parse the nodes
                BTNode rhsNode = ParseExpression(normilizedExpression.Substring(operatorIndex + 1));
                BTNode lhsNode = ParseExpression(normilizedExpression.Substring(0, operatorIndex));

                // Create the node
                newNode = new BTNode(lhsNode, operatorValue.ToString(), rhsNode);
            }

            return newNode;
        }

        private string NormilizeExpression(string arithmeticExpression)
        {
            int index = 0;
            string normilizedExpression = arithmeticExpression;

            if (IsSubExpression(arithmeticExpression[index]))
            {
                char marker = GetEndMarker(arithmeticExpression[index]);

                // Skip to the end of the sub expression
                for (int k = ++index; k < arithmeticExpression.Length; k++)
                {
                    if (arithmeticExpression[k] == marker)
                    {
                        break;
                    }
                    else
                    {
                        index++;
                    }
                }

                if (index == arithmeticExpression.Length - 1)
                {
                    // The full expression is a sub-expression, so remove the redundancy by returning the inner expression
                    normilizedExpression = arithmeticExpression.Substring(1, arithmeticExpression.Length - 2);
                }
            }

            return normilizedExpression;
        }

        private int FindOperatorIndex(string arithmeticExpression)
        {
            char indexValue = arithmeticExpression[0];
            int operatorIndex = 0;
            
            for (int index = 0; index < arithmeticExpression.Length; index++)
            {
                if (IsOperator(arithmeticExpression[index]))
                {
                    operatorIndex = index;
                    break;
                }
                else if (IsSubExpression(arithmeticExpression[index]))
                {
                    char marker = GetEndMarker(arithmeticExpression[index]);

                    // Skip to the end of the sub expression
                    for (int k = index; k < arithmeticExpression.Length; k++)
                    {
                        if (arithmeticExpression[k] == marker)
                        {
                            break;
                        }
                        else
                        {
                            index++;
                        }
                    }
                }
            }

            return operatorIndex;
        }

        private bool IsOperator(char value)
        {
            return (value == '+' || value == '-' || value == '*' || value == '/');
        }

        private bool IsSubExpression(char value)
        {
            return (value == '[' || value == '(');
        }

        private char GetEndMarker(char value)
        { 
            char endMarker = ']';

            if (value == '(')
            {
                endMarker = ')';
            }

            return endMarker;
        }

        public int EvaluateTree()
        {
            // Start with top node
            return EvaluateNode(_topNode);
        }

        private int EvaluateNode(BTNode aNode)
        {
            int finalResult = 0;

            if (aNode.LeftNode != null && aNode.RightNode != null)
            {
                switch (aNode.NodeValue)
                { 
                    case "+":
                        finalResult = EvaluateNode(aNode.LeftNode) + EvaluateNode(aNode.RightNode);
                        break;

                    case "-":
                        finalResult = EvaluateNode(aNode.LeftNode) - EvaluateNode(aNode.RightNode);
                        break;

                    case "*":
                        finalResult = EvaluateNode(aNode.LeftNode) * EvaluateNode(aNode.RightNode);
                        break;

                    case "/":
                        finalResult = EvaluateNode(aNode.LeftNode) / EvaluateNode(aNode.RightNode);
                        break;
                }
            }
            else
            {
                finalResult = Int32.Parse(aNode.NodeValue);
            }

            return finalResult;
        }
    }
}
