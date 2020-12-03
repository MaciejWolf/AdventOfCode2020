using System;
using System.IO;
using System.Linq;

namespace AdventOfCode2020.Day03
{
    class Program
    {
        static void Main(string[] args)
        {
            var fields = File.ReadAllLines("input-day3.txt")
                .Select(x => x
                    .ToCharArray()
                    .Select(c => c.ToFieldType())
                    .ToArray())
                .ToArray();

            var map = new Map(fields);

            var puzzle1 = CountTrees(map, new Slope(3, 1));

            var puzzle2 = new[]
            {
                new Slope(1, 1),
                new Slope(3, 1),
                new Slope(5, 1),
                new Slope(7, 1),
                new Slope(1, 2)
            }
            .Select(slope => CountTrees(map, slope).ToUInt64())
            .Aggregate((ulong)1, (x, y) => x * y);

            Console.WriteLine($"Puzzle1: {puzzle1}");
            Console.WriteLine($"Puzzle2: {puzzle2}");
        }

        static int CountTrees(Map map, Slope slope)
        {
            var point = new Point(0, 0);
            var trees = 0;

            while (true)
            {
                point = point with
                {
                    X = point.X + slope.Right,
                    Y = point.Y + slope.Down
                };

                var field = map.GetFieldType(point);

                if (!field.HasValue)
                {
                    break;
                }
                else if (field.Value == FieldType.Tree)
                {
                    trees++;
                }
            }

            return trees;
        }
    }
}
