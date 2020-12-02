using System.Linq;

namespace AdventOfCode2020.Day02
{
    class NewPolicy
    {
        private readonly char character;
        private readonly int firstPosition;
        private readonly int secondPosition;

        public NewPolicy(char character, int firstPosition, int secondPosition)
        {
            this.character = character;
            this.firstPosition = firstPosition;
            this.secondPosition = secondPosition;
        }

        public static NewPolicy FromString(string str)
        {
            var arr = str.Split(" ");
            var range = arr[0].Split("-");

            var first = int.Parse(range[0]);
            var second = int.Parse(range[1]);
            char character = arr[1].ToCharArray().Single();

            return new NewPolicy(character, first, second);
        }

        public bool CheckPassword(string password)
        {
            if (password.Length < firstPosition)
                return false;

            var first = password.ElementAt(firstPosition - 1) == character;

            if (password.Length < secondPosition)
                return first;

            var second = password.ElementAt(secondPosition - 1) == character;

            if (first && second)
                return false;

            return first || second;
        }
    }
}
