namespace AdventOfCode2020.Day03
{
    class Map
    {
        private readonly FieldType[][] fields;

        private int Length => fields.Length;
        private int Width => fields[0].Length;

        public Map(FieldType[][] fields)
        {
            this.fields = fields;
        }

        public FieldType? GetFieldType(Point point)
        {
            if (point.X < 0 || point.Y < 0 || point.Y > Length - 1)
                return null;

            point = point with { X = point.X % Width };

            return fields[point.Y][point.X];
        }
    }
}
