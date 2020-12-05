using AdventOfCode2020.Common.Optional;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2020.Day04
{
    class Program
    {
        static void Main(string[] args)
        {
            var passports = File.ReadAllLines("input-day4.txt")
                .GroupLines()
                .Select(line => line
                .Split(" ")
                    .Select(str => PassportField.FromString(str))
                    .ToArray())
                .Select(fields => new Passport(fields))
                .ToArray();

            var puzzle1 = passports.Count(HasAllFields);
            Console.WriteLine($"Puzzle1: {puzzle1}");

            var puzzle2 = passports.Count(IsValid);
            Console.WriteLine($"Puzzle2: {puzzle2}");
        }

        static bool HasAllFields(Passport passport) 
            => passport.GetField("byr").HasValue() &&
                passport.GetField("iyr").HasValue() &&
                passport.GetField("eyr").HasValue() &&
                passport.GetField("hgt").HasValue() &&
                passport.GetField("hcl").HasValue() &&
                passport.GetField("ecl").HasValue() &&
                passport.GetField("pid").HasValue();


        static bool IsValid(Passport passport)
            => passport.GetField("byr").HasValue(IsBirthYearValid) &&
                passport.GetField("iyr").HasValue(IsIssueYearValid) &&
                passport.GetField("eyr").HasValue(IsExpirationYearValid) &&
                passport.GetField("hgt").HasValue(IsHeightValid) &&
                passport.GetField("hcl").HasValue(IsHairColorValid) &&
                passport.GetField("ecl").HasValue(IsEyeColorValid) &&
                passport.GetField("pid").HasValue(IsPassportIdValid);

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
                .Aggregate(new StringBuilder(), (builder, c) => builder.Append(c))
                .ToString();

            return heightStr
                .SkipLast(2)
                .Aggregate(new StringBuilder(), (builder, c) => builder.Append(c))
                .ToString()
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
