using System;
using System.Linq;

namespace AdventOfCode.Day11
{
    public struct Point
    {
        public bool IsSeat;
        public bool Occupied;

        public bool IsSeatOccupied()
        {
            return IsSeat && Occupied;
        }

        public override string ToString()
        {
            if (!IsSeat) return ".";
            return Occupied ? "#" : "L";
        }
    }

    public class Traversal
    {
        public int X;
        public int Y;

        public Traversal(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    public class WaitingArea
    {
        private Point[][] _points;

        private Traversal[] _traversalCombinations =
        {
            new Traversal(-1, -1),
            new Traversal(-1, 1),
            new Traversal(-1, 0),
            new Traversal(1, -1),
            new Traversal(0, -1),
            new Traversal(0, 1),
            new Traversal(1, 1),
            new Traversal(1, 0),
        };

        private void MapWaitingArea()
        {
            foreach (var line in _points)
            {
                Console.WriteLine(string.Join("", line.Select(s => s.ToString()).ToArray()));
            }

            Console.WriteLine("----------");
        }

        public int PredictOccupiedSeats(bool onlyImmediatelyAdjacent = true, int minAdjacent = 4)
        {
            MapWaitingArea();

            var i = 0;
            while (true)
            {
                var updated = _points.DeepClone();
                for (var k = 0; k < _points.Length; k++)
                {
                    var line = _points[k];
                    for (var j = 0; j < line.Length; j++)
                    {
                        if (!updated[k][j].IsSeat)
                        {
                            continue;
                        }

                        var adj = 0;
                        foreach (var traversal in _traversalCombinations)
                        {
                            var x = j;
                            var y = k;
                            while (true)
                            {
                                y += traversal.Y;
                                if (y < 0 || y >= _points.Length)
                                {
                                    break;
                                }
                                
                                x += traversal.X;
                                if (x < 0 || x >= line.Length)
                                {
                                    break;
                                }

                                var p = _points[y][x];
                                if (p.IsSeatOccupied())
                                {
                                    adj++;
                                }

                                if (onlyImmediatelyAdjacent || p.IsSeat)
                                {
                                    break;
                                }
                            }
                        }

                        if (adj == 0)
                        {
                            updated[k][j].Occupied = true;
                        }

                        if (adj >= minAdjacent)
                        {
                            updated[k][j].Occupied = false;
                        }
                    }
                }

                if (!DidAreaChange(updated))
                {
                    break;
                }

                _points = updated;
                MapWaitingArea();
                i++;
            }

            return _points.Sum(line => line.Count(p => p.IsSeatOccupied()));
        }

        private bool DidAreaChange(Point[][] updated)
        {
            for (var j = 0; j < updated.Length; j++)
            {
                var a = string.Join("", updated[j].Select(s => s.ToString()).ToArray());
                var b = string.Join("", _points[j].Select(s => s.ToString()).ToArray());
                if (a != b)
                {
                    return true;
                }
            }

            return false;
        }

        public static WaitingArea Parse(string[] input)
        {
            var grid = new WaitingArea();
            grid._points = new Point[input.Length][];
            for (var i = 0; i < input.Length; i++)
            {
                grid._points[i] = input[i].ToCharArray()
                    .Select(c => new Point {IsSeat = c == 'L'})
                    .ToArray();
            }

            return grid;
        }
    }

    public static class Extensions
    {
        public static Point[][] DeepClone(this Point[][] input)
        {
            var points = new Point[input.Length][];
            for (var i = 0; i < input.Length; i++)
            {
                points[i] = input[i]
                    .Select(c => new Point {IsSeat = c.IsSeat, Occupied = c.Occupied})
                    .ToArray();
            }

            return points;
        }
    }
}