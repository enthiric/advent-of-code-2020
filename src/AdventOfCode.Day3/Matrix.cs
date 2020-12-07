using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Day3
{
    public class Matrix
    {
        private Node[,] Nodes;
        private int LocationX;
        private int LocationY;

        private Matrix(Node[,] nodes)
        {
            Nodes = nodes;
        }

        public bool AtRightBound()
        {
            return LocationX == Nodes.GetLength(1) - 1;
        }

        public bool AtLowerBound()
        {
            return LocationY == Nodes.GetLength(0);
        }

        public Node CurrentNode()
        {
            return Nodes[LocationY, LocationX];
        }

        public Node Traverse(IEnumerable<Direction> directions)
        {
            foreach (var direction in directions)
            {
                switch (direction)
                {
                    case Direction.Right:
                        if (AtRightBound())
                        {
                            LocationX = 0;
                        }
                        else
                        {
                            LocationX++;
                        }

                        break;
                    case Direction.Down:
                        if (!AtLowerBound())
                        {
                            LocationY++;
                        }

                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
                }
            }

            return AtLowerBound() ? null : CurrentNode();
        }

        public static Matrix Parse(string[] input)
        {
            if (input.Length < 1) throw new ArgumentException("invalid matrix input", nameof(input));

            var nodes = new Node[input.Length, input[0].Length];
            var rowN = 0;
            foreach (var row in input)
            {
                var nodeN = 0;
                foreach (var node in row.Select(n => n.ToString()).Where(node => node == "." || node == "#"))
                {
                    nodes[rowN, nodeN] = Node.Parse(node);
                    nodeN++;
                }

                rowN++;
            }

            return new Matrix(nodes);
        }
    }
}