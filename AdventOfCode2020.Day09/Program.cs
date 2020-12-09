using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020.Day09
{
    class Program
    {
        const int PreambleSize = 25;

        static void Main(string[] args)
        {
            var input = File
                .ReadAllLines("input-day9.txt")
                .Select(i => Convert.ToInt64(i))
                .ToArray();

            var invalidValue = FindInvalidValue(input);
            Console.WriteLine($"Puzzle1: {invalidValue}");

            var weakness = FindWeakness(input, invalidValue);
            var puzzle2 = weakness.Min() + weakness.Max();
            Console.WriteLine($"Puzzle2: {puzzle2}");
        }

        static IEnumerable<long> FindWeakness(ICollection<long> input, long value)
        {
            for (var i = 0; i < input.Count - 1; i++)
            {
                var args = new List<long>();
                args.Add(input.ElementAt(i));

                for (var j = i + 1; j < input.Count; j++)
                {
                    args.Add(input.ElementAt(j));

                    var sum = args.Sum();

                    if (sum == value)
                    {
                        return args;
                    }
                    else if (sum > value)
                    {
                        break;
                    }
                }
            }

            throw new Exception();
        }

        static long FindInvalidValue(ICollection<long> input)
        {
            for (var i = PreambleSize; i < input.Count; i++)
            {
                var sums = GetPreambleSums(input.Skip(i - PreambleSize).Take(PreambleSize).ToArray());


                if (!sums.Contains(input.ElementAt(i)))
                {
                    return input.ElementAt(i);
                }
            }

            throw new Exception();
        }

        static IEnumerable<long> GetPreambleSums(ICollection<long> preamble)
        {
            var sums = new HashSet<long>();

            for (var i = 0; i < preamble.Count - 1; i++)
            {
                for (var j = i + 1; j < preamble.Count; j++)
                {
                    sums.Add(preamble.ElementAt(i) + preamble.ElementAt(j));
                }
            }

            return sums;
        }
    }
}
