using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Day1
{
    public class Code
    {
        private readonly List<int> _input;

        public Code(string path = "input.txt")
        {
            _input = File
                .ReadAllLines(path)
                .Select(int.Parse).ToList();
        }

        public int Part1()
        {
            foreach (var a in _input)
            {
                foreach (var b in _input)
                {
                    if (a + b == 2020)
                    {
                        return a * b;
                    }
                }
            }

            return 0;
        }

        public int Part2()
        {
            foreach (var a in _input)
            {
                foreach (var b in _input)
                {
                    foreach (var c in _input)
                    {
                        if (a + b + c == 2020)
                        {
                            return a * b * c;
                        }
                    }
                }
            }

            return 0;
        }
    }

    public class Program
    {
        static void Main()
        {
            var code = new Code();

            var a = code.Part1();
            Console.WriteLine(a);
            var b = code.Part2();
            Console.WriteLine(b);
        }
    }
}