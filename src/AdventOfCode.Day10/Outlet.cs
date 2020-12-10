using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Day10
{
    public class Adapter
    {
        public long Jolt;

        public static Adapter Parse(long jolt)
        {
            var adapter = new Adapter
            {
                Jolt = jolt
            };

            return adapter;
        }
    }

    public class Outlet
    {
        public Adapter[] Adapters;

        public Outlet(Adapter[] adapters)
        {
            Adapters = adapters.OrderBy(s => s.Jolt).ToArray();
        }

        public long PossibleAdapterChains(long deviceJolt)
        {
            var routes = new Dictionary<long, long> {{deviceJolt, 1}};
            var jolts = new[] {Adapter.Parse(0)}.Concat(Adapters.ToArray());
            foreach (var adapter in jolts.Reverse())
            {
                routes.Add(adapter.Jolt, GetCombinations(routes, adapter.Jolt));
            }

            return routes.Last().Value;
        }

        private static long GetCombinations(Dictionary<long, long> routes, long jolt, int idx = 1)
        {
            var c = routes.ContainsKey(jolt + idx) ? routes[jolt + idx] : 0;
            if (idx != 3)
            {
                c += GetCombinations(routes, jolt, idx + 1);
            }

            return c;
        }

        public long ChainAdapters()
        {
            var counted = 0;
            var one = 0;
            var three = 0;
            var jolt = 0;

            while (counted < Adapters.Length)
            {
                counted++;
                var a = Adapters.FirstOrDefault(adapter => adapter.Jolt == jolt + 1);
                if (a != null)
                {
                    jolt = (int) a.Jolt;
                    one++;
                    continue;
                }

                var b = Adapters.FirstOrDefault(adapter => adapter.Jolt == jolt + 3);
                if (b != null)
                {
                    jolt = (int) b.Jolt;
                    three++;
                }
            }

            return one * three;
        }
    }
}