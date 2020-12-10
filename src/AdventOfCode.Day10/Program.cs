using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Day10
{
    // jolts
    // adapters have specific joltage
    // every adapter can take joltage 1,2 or 3 lower than its rating 
    // device built-in rated for 3 jolts higher than the highest-rated adapter
    // outlet has 0

    public class Code
    {
        public long Part1(List<Adapter> adapters)
        {
            var highest = Adapter.Parse(adapters.Max(s => s.Jolt));
            highest.Jolt += 3;
            adapters.Add(highest);

            return new Outlet(adapters.ToArray()).ChainAdapters();
        }

        public long Part2(List<Adapter> adapters)
        {
            var highest = Adapter.Parse(adapters.Max(s => s.Jolt));
            return new Outlet(adapters.ToArray()).PossibleAdapterChains((int) highest.Jolt + 3);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var code = new Code();
            var input = File.ReadAllLines("input.txt").Select(s => Adapter.Parse(long.Parse(s))).ToList();

            Console.WriteLine(code.Part1(input));
            Console.WriteLine(code.Part2(input));
        }
    }
}