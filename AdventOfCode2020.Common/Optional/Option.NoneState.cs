using System;

namespace AdventOfCode2020.Common.Optional
{
    public sealed partial class Option<T>
    {
        private class NoneState : IState
        {
            public TResult Match<TResult>(Func<TResult> none, Func<T, TResult> some)
                => none();

            public override bool Equals(object other)
                => other is NoneState;

            public override int GetHashCode() => 0;
        }
    }
}
