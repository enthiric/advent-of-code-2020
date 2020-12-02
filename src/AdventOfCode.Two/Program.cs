using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode.Common;

namespace AdventOfCode.Two
{
    public class Code
    {
        private readonly List<string> _input;

        public Code(string path = "input.txt")
        {
            _input = File.ReadAllLines(path).ToList();
        }

        public Code(List<string> input)
        {
            _input = input;
        }


        public int Part1()
        {
            var valid = 0;
            foreach (var password in _input)
            {
                var split = password.Split(" ");
                var split2 = split[0].Split("-");

                var letter = split[1][0].ToString();
                var word = split[2];
                var a = int.Parse(split2[0]);
                var b = int.Parse(split2[1]);

                var count = word.ToCharArray().Count(x => x.ToString() == letter);
                if (count >= a && count <= b)
                {
                    valid++;
                }
            }

            return valid;
        }

        public int Part2()
        {
            var valid = 0;
            foreach (var password in _input)
            {
                var split = password.Split(" ");
                var split2 = split[0].Split("-");

                var letter = split[1][0].ToString();
                var word = split[2];
                var a = int.Parse(split2[0]);
                var b = int.Parse(split2[1]);

                var letters = word.ToCharArray();
                if (letters[a - 1].ToString() == letter && letters[b - 1].ToString() != letter)
                {
                    valid++;
                }

                if (letters[b - 1].ToString() == letter && letters[a - 1].ToString() != letter)
                {
                    valid++;
                }
            }

            return valid;
        }
    }

    class Program
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