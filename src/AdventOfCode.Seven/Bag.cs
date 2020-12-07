using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Seven
{
    public class Bag
    {
        private readonly string _color;
        private readonly Dictionary<Bag, int> _children = new Dictionary<Bag, int>();

        private Bag(string color)
        {
            _color = color;
        }

        public bool HasBag(string color)
        {
            return _color == color || _children.Any(child => child.Key.HasBag(color));
        }

        public long CountBags()
        {
            return _children.Sum(c => c.Value) + _children.Sum(c => c.Key.CountBags() * c.Value);
        }

        public static Dictionary<string, Bag> ParseBags(string[] input)
        {
            var numeric = new Regex("^[0-9]");
            var clean = new Regex("(bags|bag|\\.)");

            var bags = new Dictionary<string, Bag>();
            foreach (var bag in input)
            {
                var exploded = bag.Split("bags contain");
                var color = exploded[0].Trim();
                var children = exploded[1].Split(",").Select(s => clean.Replace(s, "").Trim());

                if (!bags.TryGetValue(color, out _))
                {
                    bags[color] = new Bag(color);
                }

                foreach (var child in children)
                {
                    if (child.Contains("no other"))
                    {
                        continue;
                    }

                    var n = int.Parse(numeric.Match(child).Value);
                    var c = numeric.Replace(child, "").Trim();

                    var ba = bags.GetValueOrDefault(c);
                    if (ba == null)
                    {
                        ba = new Bag(c);
                        bags[c] = ba;
                    }

                    bags[color]._children.Add(ba, n);
                }
            }

            return bags;
        }
    }
}