using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Day17
{
    public class Grid_Part2
    {
        public Dictionary<int, Dictionary<int, Dictionary<int, List<Cube>>>> States =
            new Dictionary<int, Dictionary<int, Dictionary<int, List<Cube>>>>();

        public int[][] TraversalCombinations =
        {
            new int[] {0, 0, -1, -1},
            new int[] {0, 0, -1, 1},
            new int[] {0, 0, -1, 0},
            new int[] {0, 0, 1, -1},
            new int[] {0, 0, 0, -1},
            new int[] {0, 0, 0, 1},
            new int[] {0, 0, 1, 1},
            new int[] {0, 0, 1, 0},
            new int[] {0, 1, -1, -1},
            new int[] {0, 1, -1, 1},
            new int[] {0, 1, -1, 0},
            new int[] {0, 1, 1, -1},
            new int[] {0, 1, 0, -1},
            new int[] {0, 1, 0, 1},
            new int[] {0, 1, 1, 1},
            new int[] {0, 1, 1, 0},
            new int[] {0, -1, -1, -1},
            new int[] {0, -1, -1, 1},
            new int[] {0, -1, -1, 0},
            new int[] {0, -1, 1, -1},
            new int[] {0, -1, 0, -1},
            new int[] {0, -1, 0, 1},
            new int[] {0, -1, 1, 1},
            new int[] {0, -1, 1, 0},
            new int[] {0, -1, 0, 0},
            new int[] {0, 1, 0, 0},

            new int[] {1, 0, -1, -1},
            new int[] {1, 0, -1, 1},
            new int[] {1, 0, -1, 0},
            new int[] {1, 0, 1, -1},
            new int[] {1, 0, 0, -1},
            new int[] {1, 0, 0, 1},
            new int[] {1, 0, 1, 1},
            new int[] {1, 0, 1, 0},
            new int[] {1, 1, -1, -1},
            new int[] {1, 1, -1, 1},
            new int[] {1, 1, -1, 0},
            new int[] {1, 1, 1, -1},
            new int[] {1, 1, 0, -1},
            new int[] {1, 1, 0, 1},
            new int[] {1, 1, 1, 1},
            new int[] {1, 1, 1, 0},
            new int[] {1, -1, -1, -1},
            new int[] {1, -1, -1, 1},
            new int[] {1, -1, -1, 0},
            new int[] {1, -1, 1, -1},
            new int[] {1, -1, 0, -1},
            new int[] {1, -1, 0, 1},
            new int[] {1, -1, 1, 1},
            new int[] {1, -1, 1, 0},
            new int[] {1, -1, 0, 0},
            new int[] {1, 1, 0, 0},

            new int[] {-1, 0, -1, -1},
            new int[] {-1, 0, -1, 1},
            new int[] {-1, 0, -1, 0},
            new int[] {-1, 0, 1, -1},
            new int[] {-1, 0, 0, -1},
            new int[] {-1, 0, 0, 1},
            new int[] {-1, 0, 1, 1},
            new int[] {-1, 0, 1, 0},
            new int[] {-1, 1, -1, -1},
            new int[] {-1, 1, -1, 1},
            new int[] {-1, 1, -1, 0},
            new int[] {-1, 1, 1, -1},
            new int[] {-1, 1, 0, -1},
            new int[] {-1, 1, 0, 1},
            new int[] {-1, 1, 1, 1},
            new int[] {-1, 1, 1, 0},
            new int[] {-1, -1, -1, -1},
            new int[] {-1, -1, -1, 1},
            new int[] {-1, -1, -1, 0},
            new int[] {-1, -1, 1, -1},
            new int[] {-1, -1, 0, -1},
            new int[] {-1, -1, 0, 1},
            new int[] {-1, -1, 1, 1},
            new int[] {-1, -1, 1, 0},
            new int[] {-1, -1, 0, 0},
            new int[] {-1, 1, 0, 0},

            new int[] {-1, 0, 0, 0},
            new int[] {1, 0, 0, 0},
        };

        public void PrintCubeState()
        {
            Console.WriteLine("");
            foreach (var w in States.OrderBy(x => x.Key))
            {
                foreach (var z in w.Value)
                {
                    Console.WriteLine($"z={z.Key} w={w.Key}");
                    foreach (var line in z.Value)
                    {
                        Console.WriteLine(line.Value.Aggregate("", (current, cube) => current + cube));
                    }

                    Console.WriteLine("");
                }
            }

            Console.WriteLine("-------");
        }

        public int RunCycle(int runs, int ran = 0)
        {
            if (ran == 0)
            {
                PrintCubeState();
            }

            foreach (var (w, wv) in States)
            foreach (var (z, zv) in wv)
            {
                var t = new List<Cube>();
                var c = zv.First().Value.Count;
                for (var j = 0; j < c; j++)
                {
                    t.Add(new Cube('.'));
                }

                var b = new List<Cube>();
                for (var j = 0; j < c; j++)
                {
                    b.Add(new Cube('.'));
                }

                for (int i = zv.Count - 1; i >= 0; i--)
                {
                    zv[i + 1] = zv[i];
                }

                zv[0] = t;
                zv[zv.Keys.OrderBy(x => x).Last() + 1] = b;


                var tb = new List<Cube>();
                for (var j = 0; j < c; j++)
                {
                    tb.Add(new Cube('.'));
                }

                var bb = new List<Cube>();
                for (var j = 0; j < c; j++)
                {
                    bb.Add(new Cube('.'));
                }

                for (int i = zv.Count - 1; i >= 0; i--)
                {
                    zv[i + 1] = zv[i];
                }

                zv[0] = tb;
                zv[zv.Keys.OrderBy(x => x).Last() + 1] = bb;

                foreach (var v in zv)
                {
                    v.Value.Insert(0, new Cube('.'));
                    v.Value.Insert(0, new Cube('.'));
                    v.Value.Add(new Cube('.'));
                    v.Value.Add(new Cube('.'));
                }
            }

            var copy = new Dictionary<int, Dictionary<int, Dictionary<int, List<Cube>>>>();
            foreach (var (w, wv) in States)
            {
                copy[w] = new Dictionary<int, Dictionary<int, List<Cube>>>();
                foreach (var (z, zv) in wv)
                {
                    copy[w][z] = new Dictionary<int, List<Cube>>();
                    foreach (var v in zv)
                    {
                        copy[w][z][v.Key] = new List<Cube>();
                        foreach (var cube in v.Value)
                        {
                            copy[w][z][v.Key].Add(new Cube {IsActive = cube.IsActive});
                        }
                    }
                }
            }

            // new W states
            var cbsd = States.OrderBy(x => x.Key).First().Key - 1;
            States[cbsd] = new Dictionary<int, Dictionary<int, List<Cube>>>();
            States[ran + 1] = new Dictionary<int, Dictionary<int, List<Cube>>>();
            copy[cbsd] = new Dictionary<int, Dictionary<int, List<Cube>>>();
            copy[ran + 1] = new Dictionary<int, Dictionary<int, List<Cube>>>();

            var asdas = States[0].OrderBy(x => x.Key).First().Key - 1;
            States[0][asdas] = DefaultState();
            States[0][ran + 1] = DefaultState();
            copy[0][asdas] = DefaultState();
            copy[0][ran + 1] = DefaultState();

            foreach (var (k, v) in States)
            {
                if (v.Count == States[0].Count)
                {
                    continue;
                }

                foreach (var kv in States[0])
                {
                    if (v.ContainsKey(kv.Key))
                    {
                        continue;
                    }

                    v[kv.Key] = DefaultState();
                }
            }
            
            foreach (var (k, v) in copy)
            {
                if (v.Count == copy[0].Count)
                {
                    continue;
                }

                foreach (var kv in copy[0])
                {
                    if (v.ContainsKey(kv.Key))
                    {
                        continue;
                    }

                    v[kv.Key] = DefaultState();
                }
            }

            // change state
            foreach (var (wk, wv) in States)
            {
                foreach (var (zk, zv) in wv)
                {
                    for (var j = 0; j < zv.Count; j++)
                    {
                        // y-as
                        for (var k = 0; k < zv[j].Count; k++)
                        {
                            // x-as
                            var active = 0;
                            foreach (var traversal in TraversalCombinations)
                            {
                                var x = k;
                                var y = j;
                                var z = zk;
                                var w = wk;

                                w += traversal[0];
                                if (!States.ContainsKey(w))
                                {
                                    continue;
                                }

                                z += traversal[1];
                                if (!States[w].ContainsKey(z))
                                {
                                    continue;
                                }

                                y += traversal[2];
                                if (y < 0 || y >= zv.Count)
                                {
                                    continue;
                                }

                                x += traversal[3];
                                if (x < 0 || x >= zv[y].Count)
                                {
                                    continue;
                                }

                                var cube = States[w][z][y][x];
                                if (cube.IsActive)
                                {
                                    active++;
                                }
                            }

                            if (States[wk][zk][j][k].IsActive)
                            {
                                if (active != 2 && active != 3)
                                {
                                    copy[wk][zk][j][k] = new Cube('.');
                                }
                            }
                            else
                            {
                                if (active == 3)
                                {
                                    copy[wk][zk][j][k] = new Cube('#');
                                }
                            }
                        }
                    }
                }
            }

            States = copy;
            PrintCubeState();

            ran++;
            if (ran < runs)
            {
                return RunCycle(runs, ran);
            }

            var act = 0;
            foreach (var state in States)
            {
                foreach (var z in state.Value)
                {
                    foreach (var y in z.Value)
                    {
                        foreach (var v in y.Value)
                        {
                            if (v.IsActive)
                            {
                                act++;
                            }
                        }
                    }
                }
            }

            return act;
        }

        private Dictionary<int, List<Cube>> DefaultState()
        {
            var yLength = States[0][0].Count;
            var xLength = States[0][0][0].Count;
            var cubes = new Dictionary<int, List<Cube>>();
            for (var i = 0; i < xLength; i++)
            {
                for (int j = 0; j < yLength; j++)
                {
                    if (!cubes.ContainsKey(j))
                    {
                        cubes.Add(j, new List<Cube>());
                    }

                    cubes[j].Add(new Cube('.'));
                }
            }

            return cubes;
        }

        public static Grid_Part2 Parse(string[] input)
        {
            var grid = new Grid_Part2 {States = {[0] = new Dictionary<int, Dictionary<int, List<Cube>>>()}};
            grid.States[0][0] = new Dictionary<int, List<Cube>>();
            for (var i = 0; i < input.Length; i++)
            {
                grid.States[0][0][i] = input[i]
                    .Select(c => new Cube(c))
                    .ToList();
            }


            return grid;
        }
    }
}