#if !NET11_0_OR_GREATER && NETCOREAPP3_0_OR_GREATER

namespace Polyfills;

using System;
using System.Text;

static partial class Polyfill
{
    /// <summary>
    /// Returns a value indicating whether a specified <see cref="Rune"/> occurs within this string.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.string.contains?view=net-10.0
    public static bool Contains(this string target, Rune value) =>
        target.IndexOf(value.ToString(), StringComparison.Ordinal) >= 0;

    /// <summary>
    /// Returns a value indicating whether a specified <see cref="Rune"/> occurs within this string, using the specified comparison rules.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.string.contains?view=net-10.0
    public static bool Contains(this string target, Rune value, StringComparison comparisonType) =>
        target.IndexOf(value.ToString(), comparisonType) >= 0;

    /// <summary>
    /// Reports the zero-based index of the first occurrence of the specified <see cref="Rune"/> in this string.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.string.indexof?view=net-10.0
    public static int IndexOf(this string target, Rune value) =>
        target.IndexOf(value.ToString(), StringComparison.Ordinal);

    /// <summary>
    /// Determines whether the beginning of this string instance matches the specified <see cref="Rune"/>.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.string.startswith?view=net-10.0
    public static bool StartsWith(this string target, Rune value) =>
        target.StartsWith(value.ToString(), StringComparison.Ordinal);

    /// <summary>
    /// Determines whether the end of this string instance matches the specified <see cref="Rune"/>.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.string.endswith?view=net-10.0
    public static bool EndsWith(this string target, Rune value) =>
        target.EndsWith(value.ToString(), StringComparison.Ordinal);

    /// <summary>
    /// Returns a new string in which all occurrences of a specified <see cref="Rune"/> in this instance are replaced with another specified <see cref="Rune"/>.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.string.replace?view=net-10.0
    public static string Replace(this string target, Rune oldValue, Rune newValue) =>
        target.Replace(oldValue.ToString(), newValue.ToString());

    /// <summary>
    /// Splits a string into substrings based on a specified <see cref="Rune"/> delimiter.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.string.split?view=net-10.0
    public static string[] Split(this string target, Rune separator) =>
        target.Split(new[] { separator.ToString() }, StringSplitOptions.None);

    /// <summary>
    /// Removes all leading and trailing occurrences of a specified <see cref="Rune"/> from the current string.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.string.trim?view=net-10.0
    public static string Trim(this string target, Rune trimRune)
    {
        var trimStr = trimRune.ToString();
        var trimLen = trimStr.Length;
        var start = 0;
        var end = target.Length;

        while (end - start >= trimLen &&
               string.CompareOrdinal(target, start, trimStr, 0, trimLen) == 0)
        {
            start += trimLen;
        }

        while (end - start >= trimLen &&
               string.CompareOrdinal(target, end - trimLen, trimStr, 0, trimLen) == 0)
        {
            end -= trimLen;
        }

        return target.Substring(start, end - start);
    }
}

#endif
