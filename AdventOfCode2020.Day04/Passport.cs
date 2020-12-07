using AdventOfCode2020.Common.Optional;
using System.Linq;

namespace AdventOfCode2020.Day04
{
    class Passport
    {
        private readonly PassportField[] fields;

        public Passport(PassportField[] fields)
        {
            this.fields = fields;
        }

        public Option<string> GetField(string name)
        {
            return fields
                .SingleOrDefault(field => field.Name == name)
                ?.Value
                .AsOption() ?? Option.None<string>();
        }
    }
}
