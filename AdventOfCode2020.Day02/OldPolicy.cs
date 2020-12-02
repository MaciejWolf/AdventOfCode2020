using System.Linq;

namespace AdventOfCode2020.Day02
{
    class OldPolicy
    {
        private readonly char character;
        private readonly int min;
        private readonly int max;

        public OldPolicy(char character, int min, int max)
        {
            this.character = character;
            this.min = min;
            this.max = max;
        }

        public static OldPolicy FromString(string str)
        {
            var arr = str.Split(" ");
            var range = arr[0].Split("-");

            var min = int.Parse(range[0]);
            var max = int.Parse(range[1]);
            char character = arr[1].ToCharArray().Single();

            return new OldPolicy(character, min, max);
        }

        public bool CheckPassword(string password)
        {
            var count = password.Where(c => c == character).Count();

            return min <= count && count <= max;
        }
    }
}
