using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Day16
{
    public class Ticket
    {
        public long[] Values;
        public bool Invalid;

        public Ticket(long[] values)
        {
            Values = values;
        }
    }

    public class Field
    {
        public string Name;
        public long Index = -1;
        public List<long> RangeA = new List<long>();
        public List<long> RangeB = new List<long>();
    }

    public class Scanner
    {
        public Ticket Mine;
        public List<Ticket> Nearby = new List<Ticket>();
        public List<Field> Fields = new List<Field>();

        public long ScanNearbyTickets()
        {
            var invalid = 0L;
            foreach (var ticket in Nearby)
            {
                foreach (var value in ticket.Values)
                {
                    if (Fields.Any(field => field.RangeA.Contains(value) ^ field.RangeB.Contains(value))) continue;

                    ticket.Invalid = true;
                    invalid += value;
                }
            }

            return invalid;
        }

        private Dictionary<int, List<Field>> FillPossibilities(Dictionary<int, List<Field>> possibilities)
        {
            var f = Nearby.Where(t => !t.Invalid).ToList();
            f.Add(Mine);
            

            foreach (var possibility in possibilities)
            {
                //20x
                for (int i = 0; i < possibility.Value.Count; i++)
                {
                    // 20x
                    var field = possibility.Value[i];
                    var matches = true;
                    foreach (var ticket in f)
                    {
                        var v = ticket.Values[possibility.Key];
                        if (field.RangeA.Contains(v) ^ field.RangeB.Contains(v)) continue;

                        matches = false;
                        break;
                    }

                    if (!matches)
                    {
                        possibility.Value.RemoveAt(i);
                    }   
                }
            }

            foreach (var field in Fields)
            {
                var existsIn = new List<int>();
                foreach (var p in possibilities)
                {
                    if (p.Value.Contains(field))
                    {
                        existsIn.Add(p.Key);
                    }
                }

                if (existsIn.Count == 1)
                {
                    field.Index = existsIn.First();
                    possibilities.Remove(existsIn.First());
                }
            }

            if (possibilities.Count != 0)
            {
                return FillPossibilities(possibilities);
            }
            
            return possibilities;
        }

        public long MultiplyFields(string fieldNameContains)
        {
            ScanNearbyTickets();
            
            var possibilities = new Dictionary<int, List<Field>>();
            for (var i = 0; i < Fields.Count; i++)
            {
                possibilities[i] = new List<Field>();
                foreach (var field in Fields)
                {
                    possibilities[i].Add(field);
                }
            }

            var p = FillPossibilities(possibilities);

            var output = 1L;
            foreach (var field in Fields.Where(f => f.Name.Contains(fieldNameContains)))
            {
                output *= Mine.Values[field.Index];
                Console.WriteLine($"{field.Name}: {field.Index} -  {Mine.Values[field.Index]}");
            }

            return output;
        }

        public static Scanner Parse(string input)
        {
            var scanner = new Scanner();
            var exploded = input.Split("\n\n");

            var fields = exploded[0].Split('\n');
            foreach (var field in fields)
            {
                var f = new Field();

                var explode = field.Split(":");
                f.Name = explode[0];
                var ranges = explode[1].Split(" or ");

                var rangeExA = ranges[0].Split("-").Select(long.Parse);
                var rangeExB = ranges[1].Split("-").Select(long.Parse);
                for (var i = rangeExA.First(); i <= rangeExA.Last(); i++)
                {
                    f.RangeA.Add(i);
                }

                for (var i = rangeExB.First(); i <= rangeExB.Last(); i++)
                {
                    f.RangeB.Add(i);
                }

                scanner.Fields.Add(f);
            }

            scanner.Mine = new Ticket(exploded[1].Split('\n')[1].Split(",").Select(long.Parse).ToArray());

            var nearby = exploded[2].Split('\n');
            foreach (var ticket in nearby.Skip(1))
            {
                scanner.Nearby.Add(new Ticket(ticket.Split(",").Select(long.Parse).ToArray()));
            }

            return scanner;
        }
    }

    public class Code
    {
        public long Part1(string input)
        {
            return Scanner.Parse(input).ScanNearbyTickets();
        }

        public long Part2(string input)
        {
            return Scanner.Parse(input).MultiplyFields("departure");
        }
    }

    class Program
    {
        static void Main()
        {
            var input = File
                .ReadAllText("input.txt");

            Console.WriteLine(new Code().Part1(input));
            Console.WriteLine(new Code().Part2(input));
        }
    }
}