using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Day9
{
    public class Cypher
    {
        private readonly int _preamble;
        private readonly long[] _numbers;

        public Cypher(long[] input, int preamble = 25)
        {
            _preamble = preamble;
            _numbers = input;
        }

        public long Part1()
        {
            var start = 0;
            for (var i = _preamble; i < _numbers.Length; i++)
            {
                start++;

                if (FindMatch(_numbers.Skip(start).Take(_preamble).ToArray(), _numbers[i]))
                {
                    return _numbers[i];
                }
            }

            return 0;
        }

        public long Part2()
        {
            var find = Part1();
            for (var i = 0; i < _numbers.Length; i++)
            {
                var s = _numbers[i];
                var used = new List<long>
                {
                    s
                };
                foreach (var n in _numbers.Skip(i + 1))
                {
                    s += n;
                    used.Add(n);

                    if (s > find) break;
                    if (s == find) return used.Min() + used.Max();
                }
            }

            return 0;
        }

        private static bool FindMatch(long[] numbers, long find)
        {
            foreach (var a in numbers)
            {
                foreach (var b in numbers)
                {
                    if (a + b == find)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}