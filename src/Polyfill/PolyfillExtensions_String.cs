
#pragma warning disable

// ReSharper disable RedundantUsingDirective
// ReSharper disable PartialTypeWithSinglePart

using System;
using DescriptionAttribute = System.ComponentModel.DescriptionAttribute;
using System.Text;
// ReSharper disable RedundantAttributeSuffix

static partial class PolyfillExtensions
{
#if NETFRAMEWORK || NETSTANDARD2_0

    [DescriptionAttribute("https://learn.microsoft.com/en-us/dotnet/api/system.string.gethashcode#system-string-gethashcode(system-stringcomparison)")]
    public static int GetHashCode(this string target, StringComparison comparisonType) =>
        FromComparison(comparisonType).GetHashCode(target);

    static StringComparer FromComparison(StringComparison comparison) =>
        comparison switch
        {
            StringComparison.CurrentCulture => StringComparer.CurrentCulture,
            StringComparison.CurrentCultureIgnoreCase => StringComparer.CurrentCultureIgnoreCase,
            StringComparison.InvariantCulture => StringComparer.InvariantCulture,
            StringComparison.InvariantCultureIgnoreCase => StringComparer.InvariantCultureIgnoreCase,
            StringComparison.Ordinal => StringComparer.Ordinal,
            StringComparison.OrdinalIgnoreCase => StringComparer.OrdinalIgnoreCase,
        };

    public static bool Contains(this string target, string value, StringComparison comparisonType) =>
        target.IndexOf(value, comparisonType) >= 0;

    public static bool StartsWith(this string target, char value)
    {
        if (target.Length == 0)
        {
            return false;
        }

        return target[0] == value;
    }

    public static bool EndsWith(this string target, char value)
    {
        if (target.Length == 0)
        {
            return false;
        }

        var lastPos = target.Length - 1;
        return lastPos < target.Length &&
               target[lastPos] == value;
    }

    /// <summary>
    /// Splits a string into a maximum number of substrings based on a specified delimiting character and, optionally,
    /// options. Splits a string into a maximum number of substrings based on the provided character separator,
    /// optionally omitting empty substrings from the result.
    /// </summary>
    /// <param name="separator">A character that delimits the substrings in this instance.</param>
    /// <param name="options">A bitwise combination of the enumeration values that specifies whether to trim substrings
    /// and include empty substrings.</param>
    /// <returns>An array that contains at most count substrings from this instance that are delimited by separator.</returns>
    [DescriptionAttribute("https://learn.microsoft.com/en-us/dotnet/api/system.string.split?system-string-split(system-char-system-stringsplitoptions)")]
    public static string[] Split(this string target, char separator, StringSplitOptions options = StringSplitOptions.None) =>
        target.Split(new[] {separator}, options);

    /// <summary>
    /// Splits a string into a maximum number of substrings based on a specified delimiting character and, optionally,
    /// options. Splits a string into a maximum number of substrings based on the provided character separator,
    /// optionally omitting empty substrings from the result.
    /// </summary>
    /// <param name="separator">A character that delimits the substrings in this instance.</param>
    /// <param name="count">The maximum number of elements expected in the array.</param>
    /// <param name="options">A bitwise combination of the enumeration values that specifies whether to trim substrings
    /// and include empty substrings.</param>
    /// <returns>An array that contains at most count substrings from this instance that are delimited by separator.</returns>
    [DescriptionAttribute("https://learn.microsoft.com/en-us/dotnet/api/system.string.split?system-string-split(system-char-system-int32-system-stringsplitoptions)")]
    public static string[] Split(this string target, char separator, int count, StringSplitOptions options = StringSplitOptions.None) =>
        target.Split(new[] {separator}, count, options);
#endif

#if NETFRAMEWORK || NETSTANDARD2_0 || NETCOREAPP2_0
    /// <summary>
    /// Returns a value indicating whether a specified character occurs within this string.
    /// </summary>
    /// <remarks>This method performs an ordinal (case-sensitive and culture-insensitive) comparison.</remarks>
    /// <param name="value">The character to seek.</param>
    /// <returns>true if the value parameter occurs within this string; otherwise, false.</returns>
    [DescriptionAttribute("https://learn.microsoft.com/en-us/dotnet/api/system.string.contains?system-string-contains(system-char)")]
    public static bool Contains(this string target, char value) =>
        target.IndexOf(value) >= 0;
#endif
}