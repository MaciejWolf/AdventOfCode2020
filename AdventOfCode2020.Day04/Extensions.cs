using AdventOfCode2020.Common.Optional;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2020.Day04
{
    static class Extensions
    {
        public static Option<int> TryParseToInt(this string str)
            => int.TryParse(str, out var result)
            ? result.AsOption() 
            : Option.None<int>();

        public static IEnumerable<string> GroupLines(this IEnumerable<string> input)
        {
            var lines = new List<string>();
            var builder = new StringBuilder();
            foreach (var line in input)
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    builder.Append(line).Append(' ');
                }
                else
                {
                    lines.Add(builder.ToString());
                    builder = new StringBuilder();
                }
            }
            lines.Add(builder.ToString());

            return lines.Select(line => line.Trim());
        }

        public static string AggregateToString(this IEnumerable<char> chars)
            => chars
            .Aggregate(new StringBuilder(), (builder, c) => builder.Append(c))
            .ToString();

    }
}
