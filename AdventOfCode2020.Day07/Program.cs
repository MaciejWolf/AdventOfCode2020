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

            var containsDict = input
                .Select(str =>
                {
                    var arr = str.Split(" bags contain ");
                    var bag = arr[0];
                    var innerBags = arr[1];
                    return (Bag: bag, InnerBags: innerBags);
                })
                .Select(tuple =>
                {
                    var bag = tuple.Bag;
                    var innerBags = tuple.InnerBags
                        .PreprocessInfoString()
                        .Select(str => str.ParseToDictionary());

                    return (Bag: bag, InnerBags: innerBags);
                })
                .ToDictionary(tuple => tuple.Bag, tuple => tuple.InnerBags);

            var containedByDict = new Dictionary<string, ICollection<string>>();

            foreach (var kvp in containsDict)
            {
                var containingBag = kvp.Key;

                var innerBags = kvp
                    .Value
                    .Select(x => x.Keys)
                    .ValueOr(Array.Empty<string>());

                foreach (var bag in innerBags)
                {
                    if(!containedByDict.ContainsKey(bag))
                    {
                        containedByDict.Add(bag, new HashSet<string>());
                    }

                    containedByDict[bag].Add(containingBag);
                }
            }

            var puzzle1 = GetContainingBags("shiny gold", containedByDict).Count;

            Console.WriteLine($"Puzzle1: {puzzle1}");

            var puzzle2 = CountContainedBags("shiny gold", containsDict);

            Console.WriteLine($"Puzzle2: {puzzle2}");
        }

        static ICollection<string> GetContainingBags(string name, IDictionary<string, ICollection<string>> dict)
            => dict.OptionalGet(name)
                .Match(
                    none: () => Array.Empty<string>(),
                    some: containing =>
                    {
                        var result = containing;
                        foreach (var bag in containing)
                        {
                            result = result.Concat(GetContainingBags(bag, dict)).ToArray();
                        }

                        return result.Distinct().ToArray();
                    });
        

        static int CountContainedBags(string name, IDictionary<string, Option<IDictionary<string, int>>> dict)
            => dict[name]
                .Match(
                    none: () => 0,
                    some: bags =>
                        bags.Aggregate(0, (sum, kvp) =>
                        {
                            var amount = kvp.Value;
                            var bagName = kvp.Key;

                            sum += (CountContainedBags(bagName, dict) + 1) * amount;

                            return sum;
                        }));
    }

    public static class Extensions
    {
        public static Option<TV> OptionalGet<TK, TV>(this IDictionary<TK, TV> dict, TK key)
            => dict.TryGetValue(key, out var value) 
            ? value.AsOption() 
            : Option.None<TV>();


        public static Option<string> PreprocessInfoString(this string str)
            => str
                .Replace(" bag, ", ";")
                .Replace(" bags, ", ";")
                .Replace(" bag.", "")
                .Replace(" bags.", "")
                .Replace("no other", "")
                .AsOption()
                .Where(str => !string.IsNullOrWhiteSpace(str));

        public static IDictionary<string, int> ParseToDictionary(this string str)
            => str.Split(";")
                .Aggregate(new Dictionary<string, int>(), (dict, bagInfo) =>
                {
                    var arr = bagInfo.Split(" ");

                    var amount = int.Parse(arr[0]);
                    var name = $"{arr[1]} {arr[2]}";

                    dict.Add(name, amount);
                    return dict;
                });
    }
}
