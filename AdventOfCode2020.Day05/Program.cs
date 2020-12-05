using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020.Day05
{
    class Program
    {
        static void Main(string[] args)
        {
            var codes = File.ReadAllLines("input-day5.txt");

            var ids = codes
                .Select(Seat.FromCode)
                .Select(seat => seat.Id);

            var puzzle1 = ids.Max();

            Console.WriteLine($"Puzzle1: {puzzle1}");

            var possibleIds = Enumerable.Range(1, 126)
                .SelectMany(row => Enumerable.Range(0, 7)
                    .Select(column => new Seat(row, column)))
                .Select(seat => seat.Id);

            var puzzle2 = possibleIds
                .Except(ids)
                .Where(id => ids.Contains(id - 1) && ids.Contains(id + 1))
                .Single();

            Console.WriteLine($"Puzzle1: {puzzle2}");

        }
    }

    static class Extensions
    {
        public static Range GetLowerHalf(this Range range)
        {
            var middle = (range.End.Value + range.Start.Value) / 2;
            return new Range(range.Start.Value, middle);
        }

        public static Range GetUpperHalf(this Range range)
        {
            var middle = (range.End.Value + range.Start.Value) / 2;
            return new Range(middle + 1, range.End.Value);
        }
    }

    class Seat
    {
        public int Row { get; }
        public int Column { get; }
        public int Id => Row * 8 + Column;

        public Seat(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public static Seat FromCode(string code)
        {
            var vertical = code.SkipLast(3).ToArray();
            var horizontal = code.Skip(7).ToArray();

            var verticalRange = new Range(0, 127);
            var horizontalRange = new Range(0, 7);

            foreach (var c in vertical)
            {
                verticalRange = c switch
                {
                    'B' => verticalRange.GetUpperHalf(),
                    'F' => verticalRange.GetLowerHalf(),
                    _ => throw new Exception()
                };
            }

            foreach (var c in horizontal)
            {
                horizontalRange = c switch
                {
                    'R' => horizontalRange.GetUpperHalf(),
                    'L' => horizontalRange.GetLowerHalf(),
                    _ => throw new Exception()
                };
            }

            var row = verticalRange.Start.Value;
            var column = horizontalRange.Start.Value;

            return new Seat(row, column);
        }
    }
}
