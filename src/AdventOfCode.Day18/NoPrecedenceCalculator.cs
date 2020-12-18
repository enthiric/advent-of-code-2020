using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Day18
{
    public class NoPrecedenceCalculator
    {
        public void Calculate(Stack<long> numbers, Stack<Operation> operations, long n)
        {
            var op = operations.Pop();
            var num = numbers.Pop();

            switch (op)
            {
                case Operation.Add:
                    numbers.Push(num + n);
                    break;
                case Operation.Multiply:
                    numbers.Push(num * n);
                    break;
            }
        }

        public long CalculateOutcome(string input)
        {
            var numbers = new Stack<long>();
            var operations = new Stack<Operation>();
    
            foreach (var c in input.Replace(" ", ""))
            {
                if (long.TryParse(c.ToString(), out var n))
                {
                    if (operations.Count >= 1 && operations.First() != Operation.OpenParen)
                    {
                        Calculate(numbers, operations, n);
                        continue;
                    }

                    numbers.Push(n);
                    continue;
                }

                switch (c)
                {
                    case '+':
                        operations.Push(Operation.Add);
                        break;
                    case '*':
                        operations.Push(Operation.Multiply);
                        break;
                    case '(':
                        operations.Push(Operation.OpenParen);
                        break;
                    case ')':
                        if (operations.Count >= 1 && operations.First() == Operation.OpenParen)
                        {
                            operations.Pop();
                        }

                        while (operations.Count >= 1 && operations.First() != Operation.OpenParen)
                        {
                            var nu = numbers.Pop();
                            Calculate(numbers, operations, nu);   
                        }
                        break;
                }
            }

            var outcome = numbers.Pop();
            Console.WriteLine($"{input} -> {outcome}");
            return outcome;
        }
    }
}