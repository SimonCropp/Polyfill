#if !NETCOREAPP && !NETSTANDARD2_1_OR_GREATER

namespace Polyfills;

// ReSharper disable once RedundantUsingDirective
using System;
using System.Collections.Generic;
using System.ComponentModel;

static partial class Polyfill
{
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.keyvaluepair-2.deconstruct?view=net-11.0
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static void Deconstruct<TKey, TValue>(
        this KeyValuePair<TKey, TValue> target,
        out TKey key,
        out TValue value)
    {
        key = target.Key;
        value = target.Value;
    }
}
#endif
