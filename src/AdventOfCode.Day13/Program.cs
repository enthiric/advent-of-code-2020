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
            var raw = input[1].Split(",");

            var minutes = 0;
            var buses = new Dictionary<int, int>();

            foreach (var b in raw)
            {
                minutes++;
                if (b == "x")
                {
                    continue;
                }

                buses[int.Parse(b)] = minutes;
            }

            var t = 100000000000000;
            while (true)
            {
                var matches = true;
                var addition = 1;
                foreach (var bus in buses)
                {
                    addition += bus.Key;
                    
                    var start = t + bus.Value;
                    if (start % bus.Key == 0)
                    {
                        continue;
                    }

                    matches = false;
                }

                if (matches)
                {
                    break;
                }

                t += addition;
            }

            return t+1;
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