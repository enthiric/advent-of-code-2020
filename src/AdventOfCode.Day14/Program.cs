using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using MoreLinq;

namespace AdventOfCode.Day14
{
    public class Proccessor
    {
        public List<BitmaskGroup> Masks = new List<BitmaskGroup>();

        public long Process()
        {
            var swapped = new Dictionary<long, string>();
            foreach (var bitmask in Masks)
            {
                foreach (var value in bitmask.Values)
                {
                    var s = value.Value.ToArray();
                    for (var i = 0; i < bitmask.Mask.Length; i++)
                    {
                        if (bitmask.Mask[i] == 'X')
                        {
                            continue;
                        }

                        s[i] = bitmask.Mask[i];
                    }

                    swapped[value.Key] = new string(s);
                }
            }

            return swapped.Sum(x => Convert.ToInt64(x.Value, 2));
        }

        public long ProcessV2()
        {
            var swapped = new Dictionary<long, string>();
            foreach (var bitmask in Masks)
            {
                foreach (var value in bitmask.Values)
                {
                    var s = Convert.ToString(value.Key, 2).PadLeft(36, '0').ToArray();
                    for (var i = 0; i < bitmask.Mask.Length; i++)
                    {
                        if (bitmask.Mask[i] == '0')
                        {
                            continue;
                        }

                        s[i] = bitmask.Mask[i];
                    }

                    var combinations = GetCombinations(new string(s));
                    foreach (var combi in combinations)
                    {
                        swapped[combi] = value.Value;
                    }
                }
            }

            return swapped.Sum(x => Convert.ToInt64(x.Value, 2));
        }

        private long[] GetCombinations(string addr)
        {
            if (addr.All(x => x != 'X'))
            {
                return new[] {Convert.ToInt64(addr, 2)};
            }

            var s = addr.ToCharArray();
            var index = Array.IndexOf(s, 'X');

            s[index] = '0';
            var a = new string(s);
            s[index] = '1';
            var b = new string(s);

            return GetCombinations(a).Concat(GetCombinations(b)).ToArray();
        }

        public static Proccessor Parse(string[] input)
        {
            var processor = new Proccessor();
            var group = new List<string>();

            for (var i = 0; i < input.Length; i++)
            {
                group.Add(input[i]);

                if (i == input.Length - 1 || input[i + 1].StartsWith("mask"))
                {
                    processor.Masks.Add(BitmaskGroup.Parse(group.ToArray()));
                    group = new List<string>();
                }
            }

            return processor;
        }
    }

    public class BitmaskGroup
    {
        private static Regex regex = new Regex(@"\[(?<addr>\d+)\] = (?<value>\d+)");

        public string Mask;
        public Dictionary<long, string> Values = new Dictionary<long, string>();

        public static BitmaskGroup Parse(string[] input)
        {
            var mask = new BitmaskGroup();
            mask.Mask = input.First().Split(" = ").Skip(1).First();

            foreach (var value in input.Skip(1))
            {
                var groups = regex.Match(value).Groups;
                var bits = Convert.ToString(long.Parse(groups["value"].Value), 2);
                mask.Values[long.Parse(groups["addr"].Value)] = bits.PadLeft(36, '0');
            }

            return mask;
        }
    }

    public class Code
    {
        public long Part1(string[] input)
        {
            return Proccessor.Parse(input).Process();
        }

        public long Part2(string[] input)
        {
            return Proccessor.Parse(input).ProcessV2();
        }
    }

    class Program
    {
        static void Main()
        {
            var code = new Code();
            var input = File
                .ReadAllLines("input.txt")
                .ToArray();

            Console.WriteLine(code.Part1(input));
            Console.WriteLine(code.Part2(input));
        }
    }
}