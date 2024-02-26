#pragma warning disable

// ReSharper disable RedundantUsingDirective
// ReSharper disable PartialTypeWithSinglePart

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Link = System.ComponentModel.DescriptionAttribute;

[ExcludeFromCodeCoverage]
[DebuggerNonUserCode]
#if PolyPublic
public
#endif
static partial class StringPolyfill
{
    public static string Join(char separator, string[] values)
    {
#if NETSTANDARD2_0 || NETFRAMEWORK
        return string.Join(new([separator]), values);
#else
        return string.Join(separator, values);
#endif
    }

    [Link("https://learn.microsoft.com/en-us/dotnet/api/system.string.join#system-string-join(system-char-system-object())")]
    public static string Join(char separator, object[] values)
    {
#if NETSTANDARD2_0 || NETFRAMEWORK
        return string.Join(new([separator]), values);
#else
        return string.Join(separator, values);
#endif
    }

    [Link("https://learn.microsoft.com/en-us/dotnet/api/system.string.join#system-string-join(system-char-system-string()-system-int32-system-int32)")]
    public static string Join(char separator, string?[] value, int startIndex, int count)
    {
#if NETSTANDARD2_0 || NETFRAMEWORK
        return string.Join(new([separator]), value, startIndex, count);
#else
        return string.Join(separator, value, startIndex, count);
#endif
    }

    [Link("https://learn.microsoft.com/en-us/dotnet/api/system.string.join#system-string-join-1(system-char-system-collections-generic-ienumerable((-0)))")]
    public static string Join<T>(char separator, IEnumerable<T> values)
    {
#if NETSTANDARD2_0 || NETFRAMEWORK
        return string.Join(new([separator]), values);
#else
        return string.Join(separator, values);
#endif
    }
}