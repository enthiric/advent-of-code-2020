using System;
using System.IO;

namespace AdventOfCode.Day8
{
    public class Code
    {
        public int Part1(string[] input)
        {
            return BootCode.Parse(input).Part1();
        }

        public long Part2(string[] input)
        {
            return BootCode.Parse(input).Part2();
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            var code = new Code();

            Console.WriteLine(code.Part1(File.ReadAllLines("input.txt")));
            Console.WriteLine(code.Part2(File.ReadAllLines("input-2.txt")));
        }
    }
}