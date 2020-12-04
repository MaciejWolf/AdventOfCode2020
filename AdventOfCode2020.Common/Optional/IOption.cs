using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020.Common.Optional
{
    internal interface IOption<T>
    {
        TResult Match<TResult>(Func<TResult> none, Func<T, TResult> some);
    }
}
