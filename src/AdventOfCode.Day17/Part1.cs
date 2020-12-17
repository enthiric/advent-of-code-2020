using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Day17
{
    public class Grid_Part1
    {
        public Dictionary<int, Dictionary<int, List<Cube>>> States = new Dictionary<int, Dictionary<int, List<Cube>>>();

        public int[][] TraversalCombinations =
        {
            new int[] {0, -1, -1},
            new int[] {0, -1, 1},
            new int[] {0, -1, 0},
            new int[] {0, 1, -1},
            new int[] {0, 0, -1},
            new int[] {0, 0, 1},
            new int[] {0, 1, 1},
            new int[] {0, 1, 0},

            new int[] {1, -1, -1},
            new int[] {1, -1, 1},
            new int[] {1, -1, 0},
            new int[] {1, 1, -1},
            new int[] {1, 0, -1},
            new int[] {1, 0, 1},
            new int[] {1, 1, 1},
            new int[] {1, 1, 0},

            new int[] {-1, -1, -1},
            new int[] {-1, -1, 1},
            new int[] {-1, -1, 0},
            new int[] {-1, 1, -1},
            new int[] {-1, 0, -1},
            new int[] {-1, 0, 1},
            new int[] {-1, 1, 1},
            new int[] {-1, 1, 0},

            new int[] {-1, 0, 0},
            new int[] {1, 0, 0},
        };

        public void PrintCubeState()
        {
            Console.WriteLine("");
            foreach (var state in States.OrderBy(x => x.Key))
            {
                Console.WriteLine($"z={state.Key}");
                foreach (var line in state.Value)
                {
                    Console.WriteLine(line.Value.Aggregate("", (current, cube) => current + cube));
                }

                Console.WriteLine("");
            }

            Console.WriteLine("-------");
        }

        public int RunCycle(int runs, int ran = 0)
        {
            if (ran == 0)
            {
                PrintCubeState();
            }

            foreach (var (key, value) in States)
            {
                var t = new List<Cube>();
                var c = value.First().Value.Count;
                for (var j = 0; j < c; j++)
                {
                    t.Add(new Cube('.'));
                }

                var b = new List<Cube>();
                for (var j = 0; j < c; j++)
                {
                    b.Add(new Cube('.'));
                }

                for (int i = value.Count - 1; i >= 0; i--)
                {
                    value[i + 1] = value[i];
                }

                value[0] = t;
                value[value.Keys.OrderBy(x => x).Last() + 1] = b;


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

                for (int i = value.Count - 1; i >= 0; i--)
                {
                    value[i + 1] = value[i];
                }

                value[0] = tb;
                value[value.Keys.OrderBy(x => x).Last() + 1] = bb;

                foreach (var v in value)
                {
                    v.Value.Insert(0, new Cube('.'));
                    v.Value.Insert(0, new Cube('.'));
                    v.Value.Add(new Cube('.'));
                    v.Value.Add(new Cube('.'));
                }
            }

            var copy = new Dictionary<int, Dictionary<int, List<Cube>>>();
            foreach (var (key, value) in States)
            {
                copy[key] = new Dictionary<int, List<Cube>>();
                foreach (var v in value)
                {
                    copy[key][v.Key] = new List<Cube>();
                    foreach (var cube in v.Value)
                    {
                        copy[key][v.Key].Add(new Cube {IsActive = cube.IsActive});
                    }
                }
            }

            // new Z states
            var cbsd = States.OrderBy(x => x.Key).First().Key - 1;
            States[cbsd] = DefaultState();
            States[ran + 1] = DefaultState();
            copy[cbsd] = DefaultState();
            copy[ran + 1] = DefaultState();

            // change state
            foreach (var (key, value) in States)
            {
                for (var j = 0; j < value.Count; j++)
                {
                    // y-as
                    for (var k = 0; k < value[j].Count; k++)
                    {
                        // x-as
                        var active = 0;
                        foreach (var traversal in TraversalCombinations)
                        {
                            var x = k;
                            var y = j;
                            var z = key;

                            z += traversal[0];
                            if (!States.ContainsKey(z))
                            {
                                continue;
                            }

                            y += traversal[1];
                            if (y < 0 || y >= value.Count)
                            {
                                continue;
                            }

                            x += traversal[2];
                            if (x < 0 || x >= value[y].Count)
                            {
                                continue;
                            }

                            var cube = States[z][y][x];
                            if (cube.IsActive)
                            {
                                active++;
                            }
                        }

                        if (States[key][j][k].IsActive)
                        {
                            if (active != 2 && active != 3)
                            {
                                copy[key][j][k] = new Cube('.');
                            }
                        }
                        else
                        {
                            if (active == 3)
                            {
                                copy[key][j][k] = new Cube('#');
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
                foreach (var y in state.Value)
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
            
            return act;
        }

        private Dictionary<int, List<Cube>> DefaultState()
        {
            var yLength = States[0].Count;
            var xLength = States[0][0].Count;
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

        public static Grid_Part1 Parse(string[] input)
        {
            var grid = new Grid_Part1 {States = {[0] = new Dictionary<int, List<Cube>>()}};

            for (var i = 0; i < input.Length; i++)
            {
                grid.States[0][i] = input[i]
                    .Select(c => new Cube(c))
                    .ToList();
            }


            return grid;
        }
    }
}