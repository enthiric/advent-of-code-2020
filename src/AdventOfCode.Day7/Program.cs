using System;
using System.IO;
using System.Linq;

namespace AdventOfCode.Day7
{
    public class Code
    {
        public int Part1(string[] input)
        {
            return Bag.ParseBags(input).Count(b => b.Value.HasBag("shiny gold")) - 1;
        }

        public long Part2(string[] input)
        {
            return Bag.ParseBags(input)["shiny gold"].CountBags();
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