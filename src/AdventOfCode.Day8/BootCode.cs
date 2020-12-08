using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Day8
{
    public class BootCode
    {
        public List<Instruction> Instructions = new List<Instruction>();
        public int Accumulator;
        public int CurrentInstruction;

        public int Part2()
        {
            for (int i = 0; i < Instructions.Count; i++)
            {
                if (Instructions[i].Operation == Operation.Acc)
                {
                    continue;
                }

                var c = Parse(Instructions);
                c.Instructions[i].Operation =
                    c.Instructions[i].Operation == Operation.Jmp ? Operation.Nop : Operation.Jmp;

                while (true)
                {
                    if (c.Instructions.Count - 1 < c.CurrentInstruction ||
                        c.Instructions[c.CurrentInstruction].Ran >= 1)
                    {
                        break;
                    }

                    c.Next();
                }

                if (c.Instructions.Count - 1 < c.CurrentInstruction)
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
                case Operation.Acc:
                    Accumulator += instruction.Argument;
                    goto case Operation.Nop;
                case Operation.Jmp:
                    CurrentInstruction += instruction.Argument;
                    break;
                case Operation.Nop:
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