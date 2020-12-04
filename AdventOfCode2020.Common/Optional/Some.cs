using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020.Common.Optional
{
    internal class Some<T> : IOption<T>
    {
        private readonly T value;

        internal Some(T value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            this.value = value;
        }

        public TResult Match<TResult>(Func<TResult> none, Func<T, TResult> some) => some(value);

        public override bool Equals(object other)
            => other is Some<T> some && Equals(some);

        private bool Equals(Some<T> other)
            => ReferenceEquals(this, other) || value.Equals(other.value);

        public override int GetHashCode()
        {
            return HashCode.Combine(value);
        }
    }
}
