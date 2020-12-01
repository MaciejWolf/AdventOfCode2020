using System;
using System.IO;
using System.Linq;

namespace AdventOfCode2020.Day01
{
    class Program
    {
        static void Main(string[] args)
        {
            var entries = File.ReadAllLines("input-day1.txt")
                .Select(x => int.Parse(x))
                .ToArray();

            var result1 = Puzzle1(entries, 2020);
            Console.WriteLine($"Puzzle1: {result1}");

            var result2 = Puzzle2(entries, 2020);
            Console.WriteLine($"Puzzle2: {result2}");
        }

        private static int Puzzle1(int[] entries, int sum)
        {
            for (var i = 0; i < entries.Length - 1; i++)
            {
                for (var j = i + 1; j < entries.Length; j++)
                {
                    if (entries[i] + entries[j] == sum)
                    {
                        return entries[i] * entries[j];
                    }
                }
            }

            throw new Exception();
        }

        private static int Puzzle2(int[] entries, int sum)
        {
            for (var i = 0; i < entries.Length - 2; i++)
            {
                for (var j = i + 1; j < entries.Length - 1; j++)
                {
                    for (var k = i + 2; k < entries.Length; k++)
                    {
                        if (entries[i] + entries[j] + entries[k] == sum)
                        {
                            return entries[i] * entries[j] * entries[k];
                        }
                    }
                }
            }

            throw new Exception();
        }
    }
}
