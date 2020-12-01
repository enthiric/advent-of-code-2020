using System;
using System.Diagnostics;

namespace AdventOfCode.Common
{
    public static class Measure
    {
        public static T Duration<T>(Func<T> function)
        {
            var sw = Stopwatch.StartNew();
            var result = function();
            sw.Stop();

            Console.WriteLine($"Took: {sw.Elapsed.TotalMilliseconds:#,##0.000}ms ({sw.ElapsedTicks:#,##0} ticks)");

            return result;
        }
    }
}