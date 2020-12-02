using System;
using System.IO;
using System.Linq;

namespace AdventOfCode2020.Day02
{
    class Program
    {
        static void Main(string[] args)
        {
            var pairs = File.ReadAllLines("input-day2.txt")
                .Select(x =>
                {
                    var arr = x.Split(": ");

                    return new
                    {
                        PolicyString = arr[0],
                        Password = arr[1]
                    };
                })
                .ToArray();

            var puzzle1 = pairs
                .Select(pair => new
                {
                    Policy = OldPolicy.FromString(pair.PolicyString),
                    Password = pair.Password
                })
                .Count(pair => pair.Policy.CheckPassword(pair.Password));

            var puzzle2 = pairs
                .Select(pair => new
                {
                    Policy = NewPolicy.FromString(pair.PolicyString),
                    Password = pair.Password
                })
                .Count(pair => pair.Policy.CheckPassword(pair.Password));

            Console.WriteLine($"Puzzle1: {puzzle1}");
            Console.WriteLine($"Puzzle2: {puzzle2}");
        }
    }
}
