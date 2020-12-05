using System;

namespace AdventOfCode2020.Day05
{
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
}
