using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020.Common.Optional
{
    public sealed class Option<T>
    {
        private readonly IOption<T> state;

        public Option()
        {
            state = new None<T>();
        }

        public Option(T value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            state = new Some<T>(value);
        }

        private Option(IOption<T> state)
        {
            this.state = state;
        }

        public TResult Match<TResult>(Func<TResult> none, Func<T, TResult> some)
        {
            return state.Match(none, some);
        }

        public static Option<T> None() => new Option<T>(new None<T>());

        public override bool Equals(object obj)
            => state.Equals(obj);

        public override int GetHashCode()
            => state.GetHashCode();
    }
}
