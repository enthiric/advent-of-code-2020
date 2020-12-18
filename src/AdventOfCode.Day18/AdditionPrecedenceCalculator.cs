using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace AdventOfCode.Day18
{
    public class AdditionPrecedenceCalculator
    {
        private Stack<Operation> Copy(Stack<Operation> operations)
        {
            var copy = new Stack<Operation>();
            foreach (var op in operations.Reverse())
            {
                copy.Push(op);
            }

            return copy;
        }

        private void Calculate(Stack<long> numbers, Stack<Operation> operations, long n)
        {
            switch (operations.Peek())
            {
                case Operation.Add:
                    numbers.Push(numbers.Pop() + n);
                    operations.Pop();
                    break;
                case Operation.Multiply:
                    numbers.Push(n);
                    break;
            }
        }

        private void CalculateGroup(Stack<long> numbers, Stack<Operation> operations)
        {
            var internalOperations = new Stack<Operation>();
            var internetNumbers = new Stack<long>();
            while (operations.Count >= 1)
            {
                var op = operations.Pop();
                if (op == Operation.OpenParen)
                {
                    internetNumbers.Push(numbers.Pop());
                    break;
                }

                if (operations.Count == 0)
                {
                    internetNumbers.Push(numbers.Pop());
                    break;
                }

                switch (op)
                {
                    case Operation.Multiply:
                        internalOperations.Push(op);
                        internetNumbers.Push(numbers.Pop());
                        break;
                    case Operation.Add:
                        numbers.Push(numbers.Pop() + numbers.Pop());
                        break;
                }
            }

            while (internalOperations.Count >= 1)
            {
                internetNumbers.Push(internetNumbers.Pop() * internetNumbers.Pop());
                internalOperations.Pop();
            }

            numbers.Push(internetNumbers.Pop());
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
                        if (operations.Count >= 1 && !operations.Contains(Operation.OpenParen))
                        {
                            break;
                        }

                        if (operations.Count >= 1 && operations.First() == Operation.OpenParen)
                        {
                            operations.Pop();
                            break;
                        }

                        var complete = Copy(operations);
                        CalculateGroup(numbers, complete);
                        operations = complete;
                        break;
                }
            }

            var ops = new Stack<Operation>();
            var numbs = new Stack<long>();
            while (operations.Count >= 1)
            {
                if (operations.Peek() == Operation.Multiply)
                {
                    ops.Push(operations.Pop());
                    numbs.Push(numbers.Pop());
                }

                if (operations.Count == 0)
                {
                    break;
                }

                Calculate(numbers, operations, numbers.Pop());
            }

            if (ops.Count >= 1 && ops.Peek() == Operation.Multiply)
            {
                numbs.Push(numbers.Pop());
            }

            while (ops.Count >= 1)
            {
                numbs.Push(numbs.Pop() * numbs.Pop());
                ops.Pop();
            }

            if (numbs.Count == 1)
            {
                numbers.Push(numbs.Pop());
            }

            var outcome = numbers.Pop();
            Console.WriteLine($"{input} -> {outcome}");
            return outcome;
        }
    }
}