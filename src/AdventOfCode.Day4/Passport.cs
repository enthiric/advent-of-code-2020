using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Day4
{
    public class Passport
    {
        public int Byr; //(Birth Year)
        public int Iyr; //(Issue Year)
        public int Eyr; //(Expiration Year)
        public string Cid; //(Country ID)
        public string Pid; //(Passport ID)
        public string Hgt; //(Height)
        public string Hcl; //(Hair Color)
        public string Ecl; //(Eye Color)

        
        public bool IsValid_Part1()
        {
            return !string.IsNullOrEmpty(Hgt) &&
                   !string.IsNullOrEmpty(Hcl) &&
                   !string.IsNullOrEmpty(Ecl) &&
                   !string.IsNullOrEmpty(Pid) &&
                   Byr != 0 && 
                   Iyr != 0 &&
                   Eyr != 0;
        }
        
        public bool IsValid_Part2()
        {
            if (string.IsNullOrEmpty(Hgt) || !Regex.IsMatch(Hgt,"(cm|in)$") ||
                string.IsNullOrEmpty(Hcl) ||
                string.IsNullOrEmpty(Ecl) ||
                string.IsNullOrEmpty(Pid) ||
                Byr == 0 || Byr < 1920 || Byr > 2002 ||
                Iyr == 0 || Iyr < 2010 || Iyr > 2020 ||
                Eyr == 0 || Eyr < 2020 || Eyr > 2030)
            {
                return false;
            }
            
            var hgt = int.Parse(Hgt.Substring(0, Hgt.Length - 2));
            if (Hgt.EndsWith("cm"))
            {
                if (hgt < 150 || hgt > 193)
                {
                    return false;
                }
            }

            if (Hgt.EndsWith("in"))
            {
                if (hgt < 59 || hgt > 76)
                {
                    return false;
                }
            }

            var eyes = new[] {"amb", "blu", "brn", "gry", "grn", "hzl", "oth"};
            if (!eyes.Contains(Ecl))
            {
                return false;
            }

            if (Pid.Length != 9)
            {
                return false;
            }

            if (!Regex.IsMatch(Hcl, "^#[0-9,a-f]{6}$"))
            {
                return false;
            }

            return true;
        }

        public static Passport Parse(string input)
        {
            var passport = new Passport();
            var data = input.Split(new[] {" ", "\n"}, StringSplitOptions.None);
            foreach (var f in data)
            {
                var exploded = f.Trim().Split(":");
                var type = exploded[0];
                var content = exploded[1];

                foreach (var field in typeof(Passport).GetFields())
                {
                    if (type != field.Name.ToLower()) continue;

                    switch (field.FieldType)
                    {
                        case { } i when i == typeof(int):
                            field.SetValue(passport, int.Parse(content));
                            break;
                        default:
                            field.SetValue(passport, content);
                            break;
                    }
                }
            }

            return passport;
        }
    }
}