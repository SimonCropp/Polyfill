#if !NETCOREAPP && !NETSTANDARD2_1_OR_GREATER

namespace Polyfills;

// ReSharper disable once RedundantUsingDirective
using System;
using System.Collections;
using System.ComponentModel;

static partial class Polyfill
{
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.collections.dictionaryentry.deconstruct?view=net-11.0#system-collections-dictionaryentry-deconstruct(system-object@-system-object@)
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static void Deconstruct(this DictionaryEntry target, out object key, out object? value)
    {
        key = target.Key;
        value = target.Value;
    }
}

#endif
