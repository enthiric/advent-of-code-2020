using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using MoreLinq.Extensions;

namespace AdventOfCode.Six
{
    public class Code
    {
        private static IEnumerable<string> Parse(string[] input)
        {
            return string.Join("\n", input).Split("\n\n");
        }

        public Regex regex = new Regex("[a-z]");

        public int Part1(string[] input)
        {
            return Parse(input).Sum(s => regex.Matches(s).DistinctBy(s => s.Value).Count());
        }

        public int Part2(string[] input)
        {
            var data = Parse(input);
            var n = 0;
            foreach (var group in data)
            {
                var persons = group.Split("\n");
                var answered = string.Join("", persons).Distinct().ToList();
                foreach (var answers in persons)
                {
                    answered.RemoveAll(x => !answers.Contains(x));
                }
                
                n += answered.Count;
            }

            return n;
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