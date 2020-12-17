using System;
using System.IO;

namespace AdventOfCode.Day17
{
    public struct Cube
    {
        public bool IsActive;

        public Cube(char x)
        {
            IsActive = x == '#';
        }

        public override string ToString()
        {
            return IsActive ? "#" : ".";
        }
    }

    public class Code
    {
        public long Part1(string[] input)
        {
            return Grid_Part1.Parse(input).RunCycle(6);
        }

        public long Part2(string[] input)
        {
            return Grid_Part2.Parse(input).RunCycle(6);
        }
    }
    
    class Program
    {
        static void Main()
        {
            var input = File
                .ReadAllLines("input.txt");

            Console.WriteLine(new Code().Part1(input));
            Console.WriteLine(new Code().Part2(input));
        }
    }
}