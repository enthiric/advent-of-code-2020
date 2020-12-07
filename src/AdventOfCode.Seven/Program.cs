using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Seven
{
    public class Code
    {
        public Dictionary<string, Bag> Parse(string[] input)
        {
            var bags = new Dictionary<string, Bag>();
            foreach (var bag in input)
            {
                var exploded = bag.Split("bags contain");
                var color = exploded[0].ToLower().Trim();
                var children = exploded[1].Split(",").Select(s =>
                    s
                        .Replace(",", "")
                        .Replace(".", "")
                        .Replace("bags", "")
                        .Replace("bag", "")
                        .Trim());

                if (!bags.TryGetValue(color, out var b))
                {
                    bags[color] = new Bag(color);
                }

                var regex = new Regex("^[0-9]");
                foreach (var child in children)
                {
                    if (child.Contains("no other"))
                    {
                        continue;
                    }

                    var n = int.Parse(regex.Match(child).Value);
                    var c = regex.Replace(child, "").ToLower().Trim();

                    var ba = bags.GetValueOrDefault(c);
                    if (ba == null)
                    {
                        ba = new Bag(c);
                        bags[c] = ba;
                    }

                    bags[color].Children.Add(ba, n);
                    ba.Parents.Add(bags[color]);
                }
            }

            return bags;
        }

        public int Part1(string[] input)
        {
            return Parse(input).Count(b => b.Value.HasBag("shiny gold")) - 1;
        }

        public long Part2(string[] input)
        {
            return Parse(input)["shiny gold"].CountBags();
        }
    }

    //rules:
    // light red bags contain 1 bright white bag, 2 muted yellow bags.
    // dark orange bags contain 3 bright white bags, 4 muted yellow bags.
    // bright white bags contain 1 shiny gold bag.
    // muted yellow bags contain 2 shiny gold bags, 9 faded blue bags.
    // shiny gold bags contain 1 dark olive bag, 2 vibrant plum bags.
    // dark olive bags contain 3 faded blue bags, 4 dotted black bags.
    // vibrant plum bags contain 5 faded blue bags, 6 dotted black bags.
    // faded blue bags contain no other bags.
    // dotted black bags contain no other bags.
    // 
    // gold:
    //A bright white bag, which can hold your shiny gold bag directly.
    //A muted yellow bag, which can hold your shiny gold bag directly, plus some other bags.
    //A dark orange bag, which can hold bright white and muted yellow bags, either of which could then hold your shiny gold bag.
    //A light red bag, which can hold bright white and muted yellow bags, either of which could then hold your shiny gold bag.

    public class Bag
    {
        public string Color;
        public Dictionary<Bag, int> Children = new Dictionary<Bag, int>();
        public List<Bag> Parents = new List<Bag>();

        public bool HasBag(string color)
        {
            if (Color == color)
            {
                return true;
            }

            return Children.Any(child => child.Key.HasBag(color));
        }

        public long CountBags()
        {
            return Children.Sum(c => c.Value) + Children.Sum(c => c.Key.CountBags() * c.Value);
        }

        public Bag(string color)
        {
            Color = color;
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