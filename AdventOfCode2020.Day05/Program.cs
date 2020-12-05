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

            Console.WriteLine($"Puzzle2: {puzzle2}");
        }
    }
}
