#pragma warning disable

// ReSharper disable RedundantUsingDirective
// ReSharper disable PartialTypeWithSinglePart

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
[DebuggerNonUserCode]
#if PolyPublic
public
#endif
static partial class PolyString
{
    public static string Join(char separator, string[] values)
    {
#if NETSTANDARD2_0 || NETFRAMEWORK
        return string.Join(new(new[]{separator}), values);
#else
        return string.Join(separator, values);
#endif
    }

    public static string Join(char separator, object[] values)
    {
#if NETSTANDARD2_0 || NETFRAMEWORK
        return string.Join(new(new[]{separator}), values);
#else
        return string.Join(separator, values);
#endif
    }

    public static string Join (char separator, string?[] value, int startIndex, int count)
    {
#if NETSTANDARD2_0 || NETFRAMEWORK
        return string.Join(new(new[]{separator}), value, startIndex, count);
#else
        return string.Join(separator, value, startIndex, count);
#endif
    }

    public static string Join<T> (char separator, IEnumerable<T> values)
    {
#if NETSTANDARD2_0 || NETFRAMEWORK
        return string.Join(new(new[]{separator}), values);
#else
        return string.Join(separator, values);
#endif
    }
}