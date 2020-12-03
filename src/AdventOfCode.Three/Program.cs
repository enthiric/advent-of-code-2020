using System;
using System.IO;

namespace AdventOfCode.Three
{
    public class Code
    {
        public int Calculate(string[] input, Direction[] slope)
        {
            var matrix = Matrix.Parse(input);
            var trees = 0;

            while (!matrix.AtLowerBound())
            {
                var node = matrix.Traverse(slope);
                if (node == null)
                {
                    break;
                }

                if (node.IsTree())
                {
                    trees++;
                }
            }

            return trees;
        }

        public int Part1(string[] input)
        {
            return Calculate(input, new[]
            {
                Direction.Right,
                Direction.Right,
                Direction.Right,
                Direction.Down,
            });
        }

        public long Part2(string[] input)
        {
            var slopes = new[]
            {
                new[]
                {
                    Direction.Right,
                    Direction.Down,
                },
                new[]
                {
                    Direction.Right,
                    Direction.Right,
                    Direction.Right,
                    Direction.Down,
                },
                new[]
                {
                    Direction.Right,
                    Direction.Right,
                    Direction.Right,
                    Direction.Right,
                    Direction.Right,
                    Direction.Down,
                },
                new[]
                {
                    Direction.Right,
                    Direction.Right,
                    Direction.Right,
                    Direction.Right,
                    Direction.Right,
                    Direction.Right,
                    Direction.Right,
                    Direction.Down,
                },
                new[]
                {
                    Direction.Right,
                    Direction.Down,
                    Direction.Down,
                },
            };

            long trees = 1;
            foreach (var slope in slopes)
            {
                trees *= Calculate(input, slope);
            }

            return trees;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var code = new Code();
            var input = File.ReadAllLines("input.txt");

            Console.WriteLine(code.Part1(input));
            Console.WriteLine(code.Part2(input));
        }
    }
}