using System;
using System.Linq;

namespace AdventOfCode.Day11
{
    // public class Point
    // {
    //     public bool IsSeat;
    //     public bool Occupied;
    //
    //     public override string ToString()
    //     {
    //         if (!IsSeat) return ".";
    //         return Occupied ? "#" : "L";
    //     }
    // }

    public class TraverselCombination
    {
        public int X;
        public int Y;

        public TraverselCombination(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    public class Grid
    {
        public string[][] Points;

        public TraverselCombination[] TraverselCombinations =
        {
            new TraverselCombination(-1, -1),
            new TraverselCombination(-1, 1),
            new TraverselCombination(-1, 0),
            new TraverselCombination(1, -1),
            new TraverselCombination(0, -1),
            new TraverselCombination(0, 1),
            new TraverselCombination(1, 1),
            new TraverselCombination(1, 0),
        };

        public void Print()
        {
            foreach (var line in Points)
            {
                Console.WriteLine(string.Join("", line.Select(s => s.ToString()).ToArray()));
            }

            Console.WriteLine("----------");
        }

        public int Run_2()
        {
            Print();

            var i = 0;
            while (true)
            {
                var updated = Points.DeepClone();
                for (var k = 0; k < Points.Length; k++)
                {
                    var line = Points[k];
                    for (var j = 0; j < line.Length; j++)
                    {
                        if (updated[k][j] == ".")
                        {
                            continue;
                        }

                        var adj = 0;
                        foreach (var traversel in TraverselCombinations)
                        {
                            var x = j;
                            var y = k;
                            while (true)
                            {
                                y += traversel.Y;
                                if (y < 0 || y >= Points.Length)
                                {
                                    break;
                                }

                                var l = Points[y];
                                x += traversel.X;
                                if (x < 0 || x >= line.Length)
                                {
                                    break;
                                }
                                
                                var p = l[x];
                                if (p == "L")
                                {
                                    break;
                                }
                                
                                if (p == "#")
                                {
                                    adj++;
                                    break;
                                }
                            }
                        }

                        if (adj == 0)
                        {
                            updated[k][j] = "#";
                        }

                        if (adj >= 5)
                        {
                            updated[k][j] = "L";
                        }
                    }
                }

                var eq = true;
                for (var j = 0; j < updated.Length; j++)
                {
                    var a = string.Join("", updated[j].Select(s => s.ToString()).ToArray());
                    var b = string.Join("", Points[j].Select(s => s.ToString()).ToArray());
                    if (a != b)
                    {
                        eq = false;
                    }
                }

                if (eq)
                {
                    break;
                }

                Points = updated;
                Print();
                i++;
            }

            return Points.Sum(line => line.Count(p => p == "#"));
        }

        public int Run()
        {
            Print();

            var i = 0;
            while (true)
            {
                var updated = Points.DeepClone();
                for (var k = 0; k < Points.Length; k++)
                {
                    var line = Points[k];
                    for (var j = 0; j < line.Length; j++)
                    {
                        if (updated[k][j] == ".")
                        {
                            continue;
                        }

                        var adj = 0;
                        foreach (var traversel in TraverselCombinations)
                        {
                            var y = k + traversel.Y;
                            if (y >= 0 && y < Points.Length)
                            {
                                var l = Points[y];
                                var x = j + traversel.X;
                                if (x >= 0 && x < line.Length)
                                {
                                    var p = l[x];
                                    if (p == "#")
                                    {
                                        adj++;
                                    }
                                }
                            }
                        }

                        if (adj == 0)
                        {
                            updated[k][j] = "#";
                        }

                        if (adj >= 4)
                        {
                            updated[k][j] = "L";
                        }
                    }
                }

                var eq = true;
                for (var j = 0; j < updated.Length; j++)
                {
                    var a = string.Join("", updated[j].Select(s => s.ToString()).ToArray());
                    var b = string.Join("", Points[j].Select(s => s.ToString()).ToArray());
                    if (a != b)
                    {
                        eq = false;
                    }
                }

                if (eq)
                {
                    break;
                }

                Points = updated;
                Print();
                i++;
            }

            return Points.Sum(line => line.Count(p => p == "#"));
        }

        public static Grid Parse(string[] input)
        {
            var grid = new Grid();
            grid.Points = new string[input.Length][];
            for (var i = 0; i < input.Length; i++)
            {
                grid.Points[i] = input[i].ToCharArray()
                    .Select(c => c.ToString())
                    .ToArray();
            }

            return grid;
        }
    }

    public static class Extensions
    {
        public static string[][] DeepClone(this string[][] input)
        {
            var points = new string[input.Length][];
            for (var i = 0; i < input.Length; i++)
            {
                points[i] = input[i]
                    .Select(c => c)
                    .ToArray();
            }

            return points;
        }
    }
}