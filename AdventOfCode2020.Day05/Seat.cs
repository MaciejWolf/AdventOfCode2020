using System;
using System.Linq;

namespace AdventOfCode2020.Day05
{
    class Seat
    {
        public int Row { get; }
        public int Column { get; }
        public int Id => Row * 8 + Column;

        public Seat(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public static Seat FromCode(string code)
        {
            var vertical = code.SkipLast(3).ToArray();
            var horizontal = code.Skip(7).ToArray();

            var verticalRange = new Range(0, 127);
            var horizontalRange = new Range(0, 7);

            foreach (var c in vertical)
            {
                verticalRange = c switch
                {
                    'B' => verticalRange.GetUpperHalf(),
                    'F' => verticalRange.GetLowerHalf(),
                    _ => throw new Exception()
                };
            }

            foreach (var c in horizontal)
            {
                horizontalRange = c switch
                {
                    'R' => horizontalRange.GetUpperHalf(),
                    'L' => horizontalRange.GetLowerHalf(),
                    _ => throw new Exception()
                };
            }

            var row = verticalRange.Start.Value;
            var column = horizontalRange.Start.Value;

            return new Seat(row, column);
        }
    }
}
