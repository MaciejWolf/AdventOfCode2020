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
                .Select(x => x
                    .Split(" ")
                    .Where(x => !string.IsNullOrWhiteSpace(x))
                    .Select(str => PassportField.FromString(str))
                    .ToArray())
                .Select(fields => new Passport(fields))
                .ToArray();

            var puzzle1 = passports
                .Where(passport => HasAllFields(passport))
                .Count();

            Console.WriteLine($"Puzzle1: {puzzle1}");

            var puzzle2 = passports
                .Where(passport => IsValid(passport))
                .Count();

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
                .Where(p => p.GetField("byr").Where(byr => IsBirthYearValid(byr)).HasValue())
                .Where(p => p.GetField("iyr").Where(iyr => IsIssueYearValid(iyr)).HasValue())
                .Where(p => p.GetField("eyr").Where(eyr => IsExpirationYearValid(eyr)).HasValue())
                .Where(p => p.GetField("hgt").Where(hgt => IsHeightValid(hgt)).HasValue())
                .Where(p => p.GetField("hcl").Where(hcl => IsHairColorValid(hcl)).HasValue())
                .Where(p => p.GetField("ecl").Where(ecl => IsEyeColorValid(ecl)).HasValue())
                .Where(p => p.GetField("pid").Where(pid => IsPassportIdValid(pid)).HasValue())
            .HasValue();

        static bool IsBirthYearValid(string year)
        => year
                .TryParseToInt()
                .Where(v => v >= 1920 && v <= 2002)
                .HasValue();

        static bool IsIssueYearValid(string year)
            => year
                .TryParseToInt()
                .Where(v => v >= 2010 && v <= 2020)
                .HasValue();

        static bool IsExpirationYearValid(string year)
            => year
                .TryParseToInt()
                .Where(v => v >= 2020 && v <= 2030)
                .HasValue();

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
