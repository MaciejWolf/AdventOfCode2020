using System;

namespace AdventOfCode2020.Common.Optional
{
    public sealed partial class Option<T>
    {
        private interface IState
        {
            TResult Match<TResult>(Func<TResult> none, Func<T, TResult> some);
        }
    }
}
