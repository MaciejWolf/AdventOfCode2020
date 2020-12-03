using System;

namespace AdventOfCode2020.Day03
{
    static class Extensions
    {
        public static ulong ToUInt64(this int vIn)
            => Convert.ToUInt64(vIn);

        public static FieldType ToFieldType(this char c)
        {
            return c switch
            {
                '#' => FieldType.Tree,
                '.' => FieldType.OpenSquare,
                _ => throw new Exception()
            };
        }
    }
}
