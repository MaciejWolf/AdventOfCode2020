using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020.Common.Optional
{
    internal class None<T> : IOption<T>
    {
        public TResult Match<TResult>(Func<TResult> none, Func<T, TResult> some) => none();

        public override bool Equals(object other)
            => other is None<T>;

        public override int GetHashCode()
        {
            return 0;
        }
    }
}
