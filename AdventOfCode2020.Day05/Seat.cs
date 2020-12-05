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
            var row = code
                .SkipLast(3)
                .Aggregate(new Range(0, 127), (range, c) => c switch
                {
                    'B' => range.GetUpperHalf(),
                    'F' => range.GetLowerHalf(),
                    _ => throw new Exception()
                })
                .Start
                .Value;

            var column = code
                .Skip(7)
                .Aggregate(new Range(0, 7), (range, c) => c switch
                {
                    'R' => range.GetUpperHalf(),
                    'L' => range.GetLowerHalf(),
                    _ => throw new Exception()
                })
                .Start
                .Value;

            return new Seat(row, column);
        }
    }
}
