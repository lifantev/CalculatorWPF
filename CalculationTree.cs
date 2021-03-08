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

        public void ClearTree()
        {
            Current = new Node();
        }

        public NodeType CurrentType()
        {
            return Current.Type;
        }
        public string CurrentData()
        {
            return Current.Data;
        }

        public string GetLeftChildData()
        {
            return Current.LeftNode.Data;
        }

        public string GetRightChildData()
        {
            return Current.RightNode.Data;
        }

        public void ChangeCurrentValue(string data, NodeType type)
        {
            Current.SetValues(data, type);
        }
        public void ChangeCurrentValue(string data)
        {
            Current.SetValues(data, Current.Type);
        }

        public void SwapCurrentTo(string data, NodeType type)
        {
            Current.AddLeft(Current.Data, Current.Type);
            Current.SetValues(data, type);
        }

        public void Add(string data, NodeType type, bool currentChanging = true)
        {
            if (Current.Parent == null && Current.LeftNode == null)
            {
                Current.SetValues(data, type);
                return;
            }

            if (Current.LeftNode == null)
            {
                Current.AddLeft(data, type);
                return;
            }

            if (Current.RightNode == null)
            {
                if (currentChanging == true)
                    Current = Current.AddRight(data, type);
                else
                    Current.AddRight(data, type);
                return;
            }
        }

        public void ReduceCurrentTo(string data, NodeType type)
        {
            Current.SetValues(data, type);
            Current.LeftNode = null;
            Current.RightNode = null;
            Current = Current.Parent;
        }
    }
}
