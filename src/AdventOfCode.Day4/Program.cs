using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Day4
{
    public class Code
    {
        public int Part1(string[] input)
        {
            var passports = new List<Passport>();
            var data = string.Join("\n", input).Split("\n\n");
            foreach (var passport in data)
            {
                passports.Add(Passport.Parse(passport));
            }

            return passports.Count(p => p.IsValid_Part1());
        }
        
        public int Part2(string[] input)
        {
            var passports = new List<Passport>();
            var data = string.Join("\n", input).Split("\n\n");
            foreach (var passport in data)
            {
                passports.Add(Passport.Parse(passport));
            }

            return passports.Count(p => p.IsValid_Part2());
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

// - required fields
// byr (Birth Year)
// iyr (Issue Year)
// eyr (Expiration Year)
// hgt (Height)
// hcl (Hair Color)
// ecl (Eye Color)
// pid (Passport ID)
// cid (Country ID)