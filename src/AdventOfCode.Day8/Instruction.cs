using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Day8
{
    public class Instruction
    {
        public int Ran;
        public int Argument;
        public string Operation;

        public static Instruction Parse(string raw)
        {
            var exploded = raw.Split(" ");
            return new Instruction
            {
                Ran = 0,
                Operation = exploded[0],
                Argument = int.Parse(exploded[1])
            };
        }
    }

    public class BootCode
    {
        public List<Instruction> Instructions = new List<Instruction>();
        public int Accumulator = 0;
        public int CurrentInstruction = 0;

        public int Part2()
        {
            for (int i = 0; i < Instructions.Count; i++)
            {
                if (Instructions[i].Operation == "acc")
                {
                    continue;
                }

                var c = Parse(Instructions);
                c.Instructions[i].Operation = c.Instructions[i].Operation == "jmp" ? "nop" : "jmp";

                var foundIt = false;
                while (true)
                {
                    if (c.Instructions.Count - 1 < c.CurrentInstruction)
                    {
                        foundIt = true;
                        break;
                    }
                    
                    if (c.Instructions[c.CurrentInstruction].Ran >= 1)
                    {
                        break;
                    }

                    c.Next();
                }

                if (foundIt)
                {
                    return c.Accumulator;
                }
            }

            return 0;
        }

        public int Part1()
        {
            while (true)
            {
                var instruction = Instructions[CurrentInstruction];
                if (instruction.Ran >= 1)
                {
                    break;
                }

                Next();
            }

            return Accumulator;
        }

        public void Next()
        {
            var instruction = Instructions[CurrentInstruction];
            instruction.Ran++;

            switch (instruction.Operation)
            {
                case "acc":
                    Accumulator += instruction.Argument;
                    goto case "nop";
                case "jmp":
                    CurrentInstruction += instruction.Argument;
                    break;
                case "nop":
                    CurrentInstruction++;
                    break;
            }
        }

        public static BootCode Parse(string[] input)
        {
            return new BootCode {Instructions = input.Select(Instruction.Parse).ToList()};
        }

        public static BootCode Parse(List<Instruction> input)
        {
            return new BootCode
            {
                Instructions = input.Select(s => new Instruction
                {
                    Ran = s.Ran,
                    Argument = s.Argument,
                    Operation = s.Operation
                }).ToList()
            };
        }
    }
}