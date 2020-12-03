namespace AdventOfCode.Three
{
    public class Node
    {
        private bool _isTree;

        private Node(bool tree)
        {
            _isTree = tree;
        }

        public bool IsTree()
        {
            return _isTree;
        }

        public static Node Parse(string node)
        {
            return new Node(node == "#");
        }
    }
}