using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Five
{
    public class Scanner
    {
        public List<BoardingPass> Scanned = new List<BoardingPass>();

        public void Scan(string input)
        {
            double columnsMin = 0;
            double columnsMax = 7;
            double rowsMin = 0;
            double rowsMax = 127;

            foreach (var row in input.Take(7))
            {
                var n = Math.Ceiling((rowsMax - rowsMin) / 2);
                if (row == 'F')
                {
                    rowsMax -= n;
                    continue;
                }

                rowsMin += n;
            }

            foreach (var column in input.TakeLast(3))
            {
                var n = Math.Ceiling((columnsMax - columnsMin) / 2);
                if (column == 'L')
                {
                    columnsMax -= n;
                    continue;
                }

                columnsMin += n;
            }

            var pass = new BoardingPass((int)rowsMin, (int)columnsMin);
            Scanned.Add(pass);
        }

        public static Scanner ScanAll(string[] passes)
        {
            var scanner = new Scanner();
            foreach (var pass in passes)
            {
                scanner.Scan(pass);
            }

            return scanner;
        }
    }
}