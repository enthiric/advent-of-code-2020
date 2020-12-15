using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Day15
{
    public class Code
    {
        private static long Calculate(List<long> spoken, long goal)
        {
            var idx = 0;
            var previous = spoken[^1];

            var old = new Dictionary<long, long>();
            var current = spoken.ToDictionary(t => t, t => idx++ + 1);

            while (idx < goal)
            {
                if (old.ContainsKey(previous))
                {
                    previous = idx - old[previous];
                }
                else
                {
                    previous = 0;
                }

                if (current.ContainsKey(previous))
                {
                    old[previous] = current[previous];
                }

                current[previous] = idx + 1;
                idx++;
            }

            return previous;
        }

        public long Part1(IEnumerable<long> spoken)
        {
            return Calculate(spoken.ToList(), 2020);
        }

        public long Part2(IEnumerable<long> spoken)
        {
            return Calculate(spoken.ToList(), 30000000);
        }
    }

    class Program
    {
        static void Main()
        {
            var input = File
                .ReadAllText("input.txt")
                .Split(",")
                .Select(long.Parse)
                .ToList();

            Console.WriteLine(new Code().Part1(input));
            Console.WriteLine(new Code().Part2(input));
        }
    }
}