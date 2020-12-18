using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Day18
{
    

    public class Code
    {
        public long Part1(string[] input)
        {
            var calculator = new NoPrecedenceCalculator();
            return input.Sum(line => calculator.CalculateOutcome(line));
        }

        public long Part2(string[] input)
        {
            var calculator = new AdditionPrecedenceCalculator();
            return input.Sum(line => calculator.CalculateOutcome(line));
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