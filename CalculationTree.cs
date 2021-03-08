using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator
{
    // Node can have different type of meaning
    enum NodeType { number, operation, lbracket, rbracket }

    // Node consists of Data and Type
    // You can add Left and Right node to this one
    class Node
    {
        public Node(string data, NodeType type)
        {
            Data = data;
            Type = type;
        }

        public Node LeftNode { get; set; } = null;
        public Node RightNode { get; set; } = null;
        public Node Parent { get; set; } = null;

        public Node AddLeft(string data, NodeType type)
        {
            LeftNode = new Node(data, type);
            LeftNode.Parent = this;
            return LeftNode;
        }
        public Node AddRight(string data, NodeType type)
        {
            RightNode = new Node(data, type);
            RightNode.Parent = this;
            return RightNode;
        }

        public void SetValues(string data, NodeType type)
        {
            Data = data;
            Type = type;
        }

        public string Data { get; set; }
        public NodeType Type { get; set; }
    }

    // Tree consists of Root and CurrentNode, 
    // which points to the current step of action
    class CalculationTree
    {
        public Node Root { get; set; } = null;
        public Node Current { get; set; }

        public CalculationTree()
        {
            Current = Root;
        }

        public void SwapCurrentTo(string data, NodeType type)
        {
            Current.AddLeft(Current.Data, Current.Type);
            Current.SetValues(data, type);
        }

        public void Add(string data, NodeType type)
        {
            if (Current.LeftNode == null)
            {
                Current.AddLeft(data, type);
                return;
            }

            if (Current.RightNode == null)
            {
                Current = Current.AddRight(data, type);
                return;
            }
        }
    }
}
