using AdventOfCode2020.Common.Optional;

namespace AdventOfCode2020.Day04
{
    static class Extensions
    {
        public static Option<int> TryParseToInt(this string str)
            => int.TryParse(str, out var result)
            ? result.AsOption() 
            : Option<int>.None();
    }
}
