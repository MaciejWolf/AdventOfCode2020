using System;

namespace AdventOfCode2020.Common.Optional
{
    public sealed partial class Option<T>
    {
        private readonly IState state;

        public Option() => state = new NoneState();

        public Option(T value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            state = new SomeState(value);
        }

        private Option(IState state) => this.state = state;

        public TResult Match<TResult>(Func<TResult> none, Func<T, TResult> some) 
            => state.Match(none, some);

        public static Option<T> None() => new Option<T>(new NoneState());

        public override bool Equals(object obj)
            => state.Equals(obj);

        public override int GetHashCode()
            => state.GetHashCode();
    }
}
