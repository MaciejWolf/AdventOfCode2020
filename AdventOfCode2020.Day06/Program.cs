using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2020.Day06
{
    class Program
    {
        static void Main(string[] args)
        {
            var groupedAnswers = File.ReadAllLines("input-day6.txt").GroupAnswers();

            var puzzle1 = groupedAnswers
                .Select(group => group
                    .Aggregate((x, y) => x + y)
                    .Distinct()
                    .Count())
                .Sum();

            Console.WriteLine($"Puzzle1 : {puzzle1}");

            var puzzle2 = groupedAnswers
                .Select(group => group
                    .Select(x => x.ToArray())
                    .Aggregate((x, y) => x.Intersect(y).ToArray())
                    .Length)
                .Sum();

            Console.WriteLine($"Puzzle2: {puzzle2}");
        }
    }

    static class Extensions
    {
        public static IEnumerable<IEnumerable<string>> GroupAnswers(this IEnumerable<string> input)
        {
            var groupedAnswers = new List<IEnumerable<string>>();
            var group = new List<string>();
            foreach (var str in input)
            {
                if (string.IsNullOrWhiteSpace(str))
                {
                    groupedAnswers.Add(group);
                    group = new List<string>();
                }
                else
                {
                    group.Add(str);
                }
            }

            if (group.Count > 0)
            {
                groupedAnswers.Add(group);
            }

            return groupedAnswers;
        }
    }
}
