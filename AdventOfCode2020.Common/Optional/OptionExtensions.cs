using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020.Common.Optional
{
    public static class OptionExtensions
    {
        public static Option<T> AsOption<T>(this T value)
        {
            if (value == null)
            {
                return new Option<T>();
            }
            else
            {
                return new Option<T>(value);
            }
        }

        public static Option<T> NullableToOption<T>(this T? value)
            where T : struct
        {
            if (value.HasValue)
            {
                return new Option<T>(value.Value);
            }
            else
            {
                return new Option<T>();
            }
        }

        public static bool HasValue<T>(this Option<T> option) 
            => option.Match(
                none: () => false,
                some: _ => true);

        public static Option<T> WhenSome<T>(this Option<T> option, Action<T> action)
            => option.Match(
                none: () => option,
                some: v => { action(v); return option; });

        public static Option<T> WhenNone<T>(this Option<T> option, Action action)
            => option.Match(
                none: () => { action(); return option; },
                some: v => option);

        public static Option<TResult> Select<T, TResult>(this Option<T> option, Func<T, TResult> selector)
            => option.Match(
                none: () => new Option<TResult>(),
                some: v => selector(v).AsOption());

        public static Option<TResult> SelectMany<T, TResult>(this Option<T> option, Func<T, Option<TResult>> selector)
            => option.Match(
                none: () => new Option<TResult>(),
                some: v => selector(v));

        public static Option<T> Where<T>(this Option<T> option, Func<T, bool> predicate)
            => option.Match(
                none: () => option,
                some: x => predicate(x) ? option : new Option<T>());

        public static T ValueOr<T>(this Option<T> option, Func<T> getAlternativeValue)
            => option.Match(
                some: v => v,
                none: getAlternativeValue);

        public static T ValueOr<T>(this Option<T> option, T alternativeValue)
            => option.ValueOr(() => alternativeValue);

        public static T ValueOrNull<T>(this Option<T> option) where T : class
            => option.ValueOr(() => null);

        public static T? ToNullable<T>(this Option<T> option) where T : struct
            => option.Match(
                some: v => (T?)v,
                none: () => null);
    }
}
