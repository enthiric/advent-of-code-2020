using System;
using System.IO;
using System.Linq;

namespace AdventOfCode.Day9
{
    public class Code
    {
        public long Part1(long[] input)
        {
            return new Cypher(input).Part1();
        }

        public long Part2(long[] input)
        {
            return new Cypher(input).Part2();
        }
    }
    
    // first 25 -> preamble
    // after that each number is the sum of any of the numbers of 25
    
    class Program
    {
        static void Main(string[] args)
        {
            var code = new Code();
            var input = File.ReadAllLines("input.txt").Select(long.Parse).ToArray();
            
            Console.WriteLine(code.Part1(input));
            Console.WriteLine(code.Part2(input));
        }
    }
}