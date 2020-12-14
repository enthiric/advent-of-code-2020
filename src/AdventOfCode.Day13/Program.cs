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
    }

    class Program
    {
        static void Main()
        {
            var input = File
                .ReadAllLines("input.txt")
                .ToArray();

            Console.WriteLine(new Code().Part1(input));
        }
    }
}