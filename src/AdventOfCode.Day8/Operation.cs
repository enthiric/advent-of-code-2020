namespace AdventOfCode.Day8
{
    public enum Operation
    {
        Unknown,
        Acc,
        Jmp,
        Nop
    }

    public static class OperationExtensions
    {
        public static Operation ParseAsOperation(this string op)
        {
            return op switch
            {
                "jmp" => Operation.Jmp,
                "nop" => Operation.Nop,
                "acc" => Operation.Acc,
                _ => Operation.Unknown
            };
        }
    }
}