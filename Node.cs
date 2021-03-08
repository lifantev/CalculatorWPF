namespace Calculator
{
    // Node consists of Data and Type
    // You can add Left and Right node to this one
    public class Node
    {
        public Node(string data, NodeType type)
        {
            Data = data;
            Type = type;
        }
        public Node()
        {
            Data = default;
            Type = default;
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
}
