using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator
{
    // Tree consists of Root and CurrentNode, 
    // which points to the current step of action
    public class CalculationTree
    {
        public Node Current { get; set; } = null;

        public CalculationTree()
        {
            Current = new Node();
        }

        public void ChangeCurrentValue(string data, NodeType type)
        {
            Current.SetValues(data, type);
        }

        public void SwapCurrentTo(string data, NodeType type)
        {
            Current.AddLeft(Current.Data, Current.Type);
            Current.SetValues(data, type);
        }

        public void Add(string data, NodeType type)
        {
            if (Current.Parent == null)
            {
                Current.SetValues(data, type);
            }

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

        public void ReduceCurrentTo(string data, NodeType type)
        {
            Current.SetValues(data, type);
            Current.LeftNode = null;
            Current.RightNode = null;
        }
    }
}
