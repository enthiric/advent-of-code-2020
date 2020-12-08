namespace AdventOfCode.Day8
{
    public class Instruction
    {
        public int Ran;
        public int Argument;
        public Operation Operation;

        public static Instruction Parse(string raw)
        {
            var exploded = raw.Split(" ");
            return new Instruction
            {
                Ran = 0,
                Operation = exploded[0].ParseAsOperation(),
                Argument = int.Parse(exploded[1])
            };
        }
    }
}