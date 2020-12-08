using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020.Day08
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input-day8.txt")
                .ToArray();

            var instructions = ToInstructions(input);

            var result = RunCode(instructions);

            Console.WriteLine($"Puzzle1: {result.Acc}");

            var indexes = result.InvokedInstructions
                .Where(i => !input[i].Contains("acc"));

            var factory = new OperationFactory();

            foreach (var index in indexes)
            {
                var code = input[index];

                var arr = code.Replace("+", "").Split(" ");

                var op = factory.Create(arr[0] == "nop" ? "jmp" : "nop");
                var ins = new Instruction(0, op, int.Parse(arr[1]));

                var instruction = instructions[index];
                instructions[index] = ins;

                var r = RunCode(instructions);

                if (r.Last == null)
                {
                    Console.WriteLine($"Puzzle2: {r.Acc}");
                    break;
                }

                instructions[index] = instruction;
            }
        }

        static Instruction[] ToInstructions(string[] input)
        {
            var factory = new OperationFactory();

            var instructions = input
                .Select((str, id) =>
                {
                    var arr = str.Replace("+", "").Split(" ");

                    var operationCode = arr[0];
                    var arg = int.Parse(arr[1]);

                    var operation = factory.Create(operationCode);

                    return new Instruction(id, operation, arg);
                }).ToArray();

            return instructions;
        }

        static ExecutionResult RunCode(Instruction[] instructions)
        {
            var invokedInstructions = new List<int>();

            int? last = null;
            var acc = 0;
            var position = 0;

            while (true)
            {
                if (invokedInstructions.Contains(position))
                {
                    last = position;
                    break;
                }

                var result = instructions[position].Execute(position, acc);

                invokedInstructions.Add(position);
                acc = result.Accumulator;

                if (position == instructions.Length - 1)
                {
                    break;
                }

                position = result.Position;
            }

            return new ExecutionResult(acc, invokedInstructions, last);
        }
    }

    record ExecutionResult(int Acc, List<int> InvokedInstructions, int? Last);

    class OperationFactory
    {
        public IOperation Create(string str) 
            => str switch
            {
                "acc" => new Acc(),
                "jmp" => new Jmp(),
                "nop" => new Nop(),
                _ => throw new Exception()
            };
    }

    record OperationResult(int Position, int Accumulator);

    interface IOperation
    {
        OperationResult Execute(int position, int acc, int arg);
    }

    class Acc : IOperation
    {
        public OperationResult Execute(int position, int acc, int arg) 
            => new OperationResult(position + 1, acc + arg);
    }

    class Jmp : IOperation
    {
        public OperationResult Execute(int position, int acc, int arg)
            => new OperationResult(position + arg, acc);
    }

    class Nop : IOperation
    {
        public OperationResult Execute(int position, int acc, int arg)
            => new OperationResult(position + 1, acc);
    }

    class Instruction
    {
        public int Id { get; }
        private readonly IOperation operation;
        private readonly int argument;

        public Instruction(int id, IOperation operation, int argument)
        {
            Id = id;
            this.operation = operation;
            this.argument = argument;
        }

        public OperationResult Execute(int position, int acc) 
            => operation.Execute(position, acc, argument);
    }
}
