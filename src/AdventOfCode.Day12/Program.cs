using System;
using System.IO;
using System.Linq;

namespace AdventOfCode.Day12
{
    public class Code
    {
        public long Part1(string[] input)
        {
            var computer =  NavigationalComputer.Parse(input);
            var current = new Coordinate(0, 0);
            var direction = Direction.E;
            
            foreach (var instruction in computer.Instructions)
            {
                var (coordinate, shipDirection) = computer.FollowOriginal(current, direction, instruction);
                current = coordinate;
                direction = shipDirection;
            }

            return Math.Abs(current.X) + Math.Abs(current.Y); // 582
        }

        public long Part2(string[] input)
        {
            var computer =  NavigationalComputer.Parse(input);
            var current = new Coordinate(0, 0);
            var waypoint = new Coordinate(10, -1);
            var direction = Direction.E;
            
            foreach (var instruction in computer.Instructions)
            {
                var (cur, way, shipDirection) = computer.FollowWaypoint(current, waypoint, direction, instruction);
                current = cur;
                waypoint = way;
                direction = shipDirection;
            }

            return Math.Abs(current.X) + Math.Abs(current.Y); // 52069
        }
    }

    class Program
    {
        static void Main()
        {
            var code = new Code();
            var input = File.ReadAllLines("input.txt").ToArray();

            Console.WriteLine(code.Part1(input));
            Console.WriteLine(code.Part2(input));
        }
    }
}