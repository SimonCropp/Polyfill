
#pragma warning disable

namespace Polyfills;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Link = System.ComponentModel.DescriptionAttribute;

[ExcludeFromCodeCoverage]
[DebuggerNonUserCode]
static partial class StringPolyfill
{
    /// <summary>
    /// Concatenates an array of strings, using the specified separator between each member.
    /// </summary>
    [Link("https://learn.microsoft.com/en-us/dotnet/api/system.string.join#system-string-join(system-char-system-string())")]
    public static string Join(char separator, string[] values) =>
        string.Join(separator, values);

    /// <summary>
    /// Concatenates the string representations of an array of objects, using the specified separator between each member.
    /// </summary>
    [Link("https://learn.microsoft.com/en-us/dotnet/api/system.string.join#system-string-join(system-char-system-object())")]
    public static string Join(char separator, object[] values) =>
        string.Join(separator, values);

    /// <summary>
    /// Concatenates the specified elements of a string array, using the specified separator between each element.
    /// </summary>
    [Link("https://learn.microsoft.com/en-us/dotnet/api/system.string.join#system-string-join(system-char-system-string()-system-int32-system-int32)")]
    public static string Join(char separator, string?[] value, int startIndex, int count) =>
        string.Join(separator, value, startIndex, count);

    /// <summary>
    /// Concatenates the specified elements of a string array, using the specified separator between each element.
    /// </summary>
    [Link("https://learn.microsoft.com/en-us/dotnet/api/system.string.join#system-string-join-1(system-char-system-collections-generic-ienumerable((-0)))")]
    public static string Join<T>(char separator, IEnumerable<T> values) =>
        string.Join(separator, values);
}