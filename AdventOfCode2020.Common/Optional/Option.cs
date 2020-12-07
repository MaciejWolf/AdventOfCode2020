using System;

namespace AdventOfCode2020.Common.Optional
{
    public struct Option
    {
        public static Option<T> Of<T>(T value) 
            => new Option<T>(value);

        public static Option<T> None<T>()
            => new Option<T>();
    }

    public sealed partial class Option<T>
    {
        private readonly IState state;

        internal Option() => state = new NoneState();

        internal Option(T value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            state = new SomeState(value);
        }

        public TResult Match<TResult>(Func<TResult> none, Func<T, TResult> some) 
            => state.Match(none, some);

        public override bool Equals(object obj)
            => state.Equals(obj);

        public override int GetHashCode()
            => state.GetHashCode();
    }
}
