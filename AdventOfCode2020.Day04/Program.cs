using AdventOfCode2020.Common.Optional;
using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2020.Day04
{
    class Program
    {
        static void Main(string[] args)
        {
            var passports = File.ReadAllText("input-day4.txt")
                .Split("\n\n")
                .Select(x => x.Replace('\n', ' '))
                .Select(line => line
                    .Split(" ")
                    .Where(str => !string.IsNullOrWhiteSpace(str))
                    .Select(str => PassportField.FromString(str))
                    .ToArray())
                .Select(fields => new Passport(fields))
                .ToArray();

            var puzzle1 = passports
                .Count(passport => HasAllFields(passport));

            Console.WriteLine($"Puzzle1: {puzzle1}");

            var puzzle2 = passports
                .Count(passport => IsValid(passport));

            Console.WriteLine($"Puzzle2: {puzzle2}");
        }

        static bool HasAllFields(Passport passport)
            => passport.GetField("byr").HasValue()
                && passport.GetField("iyr").HasValue()
                && passport.GetField("eyr").HasValue()
                && passport.GetField("hgt").HasValue()
                && passport.GetField("hcl").HasValue()
                && passport.GetField("ecl").HasValue()
                && passport.GetField("pid").HasValue();

        static bool IsValid(Passport passport)
            => passport
                .AsOption()
                .Where(p => p.GetField("byr").HasValue(byr => IsBirthYearValid(byr)))
                .Where(p => p.GetField("iyr").HasValue(iyr => IsIssueYearValid(iyr)))
                .Where(p => p.GetField("eyr").HasValue(eyr => IsExpirationYearValid(eyr)))
                .Where(p => p.GetField("hgt").HasValue(hgt => IsHeightValid(hgt)))
                .Where(p => p.GetField("hcl").HasValue(hcl => IsHairColorValid(hcl)))
                .Where(p => p.GetField("ecl").HasValue(ecl => IsEyeColorValid(ecl)))
                .Where(p => p.GetField("pid").HasValue(pid => IsPassportIdValid(pid)))
                .HasValue();

        static bool IsBirthYearValid(string year)
            => year
                .TryParseToInt()
                .HasValue(v => v >= 1920 && v <= 2002);

        static bool IsIssueYearValid(string year)
            => year
                .TryParseToInt()
                .HasValue(v => v >= 2010 && v <= 2020);

        static bool IsExpirationYearValid(string year)
            => year
                .TryParseToInt()
                .HasValue(v => v >= 2020 && v <= 2030);

        static bool IsHeightValid(string heightStr)
        {
            var unit = heightStr
                .TakeLast(2)
                .Aggregate("", (x, y) => x + y);

            return heightStr
                .SkipLast(2)
                .Aggregate("", (x, y) => x + y)
                .TryParseToInt()
                .Match(
                    none: () => false,
                    some: v => unit switch
                    {
                        "cm" => v >= 150 && v <= 193,
                        "in" => v >= 59 && v <= 76,
                        _ => false
                    });
        }

        static bool IsHairColorValid(string hairColor)
        {
            var rx = new Regex("^#[a-z0-9]{6}$");
            return rx.IsMatch(hairColor);
        }

        static bool IsEyeColorValid(string eyeColor)
        {
            var validEyeColors = new[] { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };
            return validEyeColors.Contains(eyeColor);
        }

        static bool IsPassportIdValid(string pid)
        {
            var rx = new Regex("^[0-9]{9}$");
            return rx.IsMatch(pid);
        }
    }
}
