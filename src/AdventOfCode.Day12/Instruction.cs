using System;

namespace AdventOfCode.Day12
{
    public enum Direction
    {
        N,
        S,
        E,
        W,
        L,
        F,
        R
    }

    public struct Instruction
    {
        public Direction Direction;
        public int Value;

        public static Instruction Parse(string input)
        {
            return new Instruction
            {
                Direction = Enum.Parse<Direction>(input[0].ToString()),
                Value = int.Parse(input.Substring(1))
            };
        }
    }
}