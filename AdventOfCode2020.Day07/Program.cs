using AdventOfCode2020.Common.Optional;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020.Day07
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input-day7.txt").ToArray();

            var bagContainsDict = input
                .Select(str =>
                {
                    var arr = str.Split(" bags contain ");
                    var bag = arr[0];
                    var containedBags = arr[1];
                    return (bag, containedBags);
                })
                .Select(pair =>
                {
                    var bag = pair.bag;
                    var containedBags = pair.containedBags
                        .Parse()
                        .Select(str => str.Parse2());

                    return (bag, containedBags);
                })
                .ToDictionary(pair => pair.bag, pair => pair.containedBags);

            var bagIsContainedDict = new Dictionary<string, ICollection<string>>();

            foreach (var kvp in bagContainsDict)
            {
                var containingBag = kvp.Key;

                var containedBags = kvp
                    .Value
                    .Select(x => x.Keys)
                    .ValueOr(Array.Empty<string>());

                foreach (var bag in containedBags)
                {
                    if (bagIsContainedDict.ContainsKey(bag))
                    {
                        bagIsContainedDict[bag].Add(containingBag);
                    }
                    else
                    {
                        bagIsContainedDict.Add(bag, new HashSet<string>());
                        bagIsContainedDict[bag].Add(containingBag);
                    }
                }
            }

            var puzzle1 = Calculate2("shiny gold", bagIsContainedDict).Distinct().Count();

            Console.WriteLine($"Puzzle1: {puzzle1}");

            var puzzle2 = Calculate("shiny gold", bagContainsDict);

            Console.WriteLine($"Puzzle2: {puzzle2}");
        }

        static ICollection<string> Calculate2(string name, IDictionary<string, ICollection<string>> dict)
        {
            if (!dict.TryGetValue(name, out var containing))
                return Array.Empty<string>();

            foreach (var bag in containing)
            {
                containing = containing.Concat(Calculate2(bag, dict)).ToArray();
            }

            return containing;
        }

        static int Calculate(string name, IDictionary<string, Option<IDictionary<string, int>>> dict)
        {
            var opt = dict[name];

            return opt.Match(
                none: () => 0,
                some: bags =>
                {
                    var sum = 0;
                    foreach (var kvp in bags)
                    {
                        var amount = kvp.Value;
                        var bagName = kvp.Key;

                        sum += (Calculate(bagName, dict) + 1) * amount;
                    }

                    return sum;
                });
        }
    }

    public static class Extensions
    {
        public static Option<string> Parse(this string str) 
            => str
                .Replace(" bag, ", ";")
                .Replace(" bags, ", ";")
                .Replace(" bag.", "")
                .Replace(" bags.", "")
                .Replace("no other", "")
                .AsOption()
                .Where(str => !string.IsNullOrWhiteSpace(str));

        public static IDictionary<string, int> Parse2(this string str)
        {
            var dict = new Dictionary<string, int>();

            var bags = str.Split(";");

            foreach (var bag in bags)
            {
                var arr = bag.Split(" ");

                var quantity = int.Parse(arr[0]);
                var name1 = arr[1];
                var name2 = arr[2];

                var name = $"{name1} {name2}";

                dict.Add(name, quantity);
            }

            return dict;
        }
    }
}
