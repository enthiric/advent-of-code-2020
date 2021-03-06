﻿using System;
using System.IO;
using System.Linq;

namespace AdventOfCode.Day11
{
    public class Code
    {
        public long Part1(string[] input)
        {
            return WaitingArea.Parse(input).PredictOccupiedSeats();
        }

        public long Part2(string[] input)
        {
            return WaitingArea.Parse(input).PredictOccupiedSeats(false, 5);
        }
    }

    class Program
    {
        static void Main()
        {
            var code = new Code();
            var input = File.ReadAllLines("input.txt").ToArray();

            Console.WriteLine(code.Part1(input));
            Console.WriteLine(code.Part2(input));
        }
    }
}