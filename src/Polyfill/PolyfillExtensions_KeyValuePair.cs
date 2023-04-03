#if NETFRAMEWORK || NETSTANDARD2_0

#pragma warning disable

// ReSharper disable RedundantUsingDirective
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable UnusedMember.Global
// ReSharper disable RedundantAttributeSuffix

using System;
using System.Collections.Generic;
using DescriptionAttribute = System.ComponentModel.DescriptionAttribute;

static partial class PolyfillExtensions
{
    /// <summary>
    /// Deconstructs the current <see cref="KeyValuePair<TKey,TValue>"/>
    /// </summary>
    /// <param name="key">The key of the current <see cref="KeyValuePair<TKey,TValue>"/>.</param>
    /// <param name="value">The value of the current <see cref="KeyValuePair<TKey,TValue>"/>.</param>
    [DescriptionAttribute("https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.keyvaluepair-2.deconstruct")]
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