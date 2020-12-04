namespace AdventOfCode2020.Day04
{
    record PassportField(string Name, string Value)
    {
        public static PassportField FromString(string str)
        {
            var args = str.Split(":");
            return new PassportField(args[0], args[1]);
        }
    }
}
