using System;

namespace AdventOfCode2020.Common.Optional
{
    public sealed partial class Option<T>
    {
        private class SomeState : IState
        {
            private readonly T value;

            public SomeState(T value)
            {
                this.value = value;
            }

            public TResult Match<TResult>(Func<TResult> none, Func<T, TResult> some)
            {
                return some(value);
            }

            public override bool Equals(object other)
                => other is SomeState some && Equals(some);

            private bool Equals(SomeState other)
                => ReferenceEquals(this, other) || value.Equals(other.value);

            public override int GetHashCode()
            {
                return HashCode.Combine(value);
            }
        }
    }
}
