using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Day13
{
    public class Code
    {
        public long Part1(string[] input)
        {
            var timestamp = int.Parse(input[0]);
            var raw = input[1].Split(",");
            var buses = raw.Where(x => x != "x").Select(int.Parse);

            var arrivals = new Dictionary<int, int>();
            foreach (var bus in buses)
            {
                var time = timestamp;
                while (true)
                {
                    if (time % bus != 0)
                    {
                        time++;
                        continue;
                    }

                    arrivals[bus] = time;
                    break;
                }
            }

            var earliest = arrivals.OrderBy(kvp => kvp.Value).First();
            return earliest.Key * (earliest.Value - timestamp);
        }

        public long Part2(string[] input)
        {
            var raw = input[1].Split(",").ToList();
            var busses = raw.Where(x => x != "x").Select(int.Parse).ToList();
            var remainders = new List<int>(busses.Count);
            
            for (var i = 0; i < raw.Count; i++)
            {
                if (raw[i] != "x")
                {
                    remainders.Add(i % busses[remainders.Count]);
                }
            }

            long t = 0;
            long addition = busses[0];

            var run = 0;
            while (true)
            {
                var matches = true;
                for (var i = run + 1; i < busses.Count; i++)
                {
                    if (Calculate(busses[i], t) != remainders[i])
                    {
                        matches = false;
                        break;
                    }

                    addition *= busses[i];
                    run = i;
                }

                if (matches)
                {
                    break;
                }

                t += addition;
            }

            return t;
        }

        private int Calculate(int bus, long time)
        {
            var left = (int) (time % bus);
            return left == 0 ? left : bus - left;
        }
    }

    class Program
    {
        static void Main()
        {
            var input = File
                .ReadAllLines("input.txt")
                .ToArray();

            Console.WriteLine(new Code().Part1(input));
            Console.WriteLine(new Code().Part2(input));
        }
    }
}