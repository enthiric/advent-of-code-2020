using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Day9
{
    public class Cypher
    {
        public int Preamble;
        public long[] Numbers;

        public long Part2() 
        {
            var find = Part1();
            for (var i = 0; i < Numbers.Length; i++)
            {
                long x = Numbers[i];
                var used = new List<long>()
                {
                    Numbers[i]
                };
                foreach (var n in Numbers.Skip(i+1))
                {
                    x += n;
                    used.Add(n);
                    if (x == find)
                    {
                        return used.Min() + used.Max();
                    }
                    
                    if (x > find)
                    {
                        break;
                    }
                }
            }

            return 0;
        }

        public long Part1() // 1492208709
        {
            var start = 0;
            for (var i = Preamble; i < Numbers.Length; i++)
            {
                var toSum = Numbers.Skip(start).Take(Preamble).ToArray();
                start++;

                var isValid = false;
                var shouldSum = Numbers[i];
                foreach (var a in toSum)
                {
                    foreach (var b in toSum)
                    {
                        if (a + b == shouldSum)
                        {
                            isValid = true;
                        }
                    }
                }

                if (!isValid)
                {
                    return shouldSum;
                }
            }

            return 0;
        }

        public Cypher(long[] input, int preamble = 25)
        {
            Preamble = preamble;
            Numbers = input;
        }
    }
}