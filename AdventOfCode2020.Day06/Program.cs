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
            var puzzle1 = File.ReadAllLines("input-day6.txt")
                .GroupAnswers()
                .Select(x => x
                    .ToCharArray()
                    .Distinct()
                    .Count())
                .Sum();

            Console.WriteLine($"Puzzle1 : {puzzle1}");

            var puzzle2 = File.ReadAllLines("input-day6.txt")
                .GroupAnswers2()
                .Select(x => x.Length)
                .Sum();

            Console.WriteLine($"Puzzle2: {puzzle2}");
        }
    }

    static class Extensions
    {
        public static IEnumerable<string> GroupAnswers(this IEnumerable<string> input)
        {
            var answers = new List<string>();
            var sb = new StringBuilder();
            foreach(var str in input)
            {
                if (string.IsNullOrWhiteSpace(str))
                {
                    answers.Add(sb.ToString());
                    sb.Clear();
                }
                else
                {
                    sb.Append(str);
                }
            }

            if (sb.Length > 0)
            {
                answers.Add(sb.ToString());
            }

            return answers;
        }

        public static IEnumerable<char[]> GroupAnswers2(this IEnumerable<string> input)
        {
            var a = new List<string>();
            var uniqueAnswers = new List<char[]>();
            foreach (var str in input)
            {
                if (string.IsNullOrWhiteSpace(str))
                {
                    var aa = a
                        .Select(x => x.ToCharArray())
                        .Aggregate((x, y) => x.Intersect(y).ToArray());

                    uniqueAnswers.Add(aa);

                    a.Clear();
                }
                else
                {
                    a.Add(str);
                }
            }

            if (a.Count > 0)
            {
                var ab = a
                        .Select(x => x.ToCharArray())
                        .Aggregate((x, y) => x.Intersect(y).ToArray());

                uniqueAnswers.Add(ab);
            }

            

            return uniqueAnswers;
        }
    }
}
