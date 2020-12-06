using System;
using System.IO;
using System.Linq;

namespace AdventOfCode.Five
{
    public class Code
    {
        public int Part1(string[] input)
        {
            return Scanner.ScanAll(input).Scanned.Max(p => p.GetSeat());
        }

        public int Part2(string[] input)
        {
            var passes = Scanner.ScanAll(input).Scanned.OrderBy(s => s.GetSeat()).ToArray();
            var last = passes.First();
            foreach (var pass in passes.Skip(1))
            {
                if (last.GetSeat() != pass.GetSeat() - 1)
                {
                    return pass.GetSeat() - 1;
                }

                last = pass;
            }

            return 0;
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