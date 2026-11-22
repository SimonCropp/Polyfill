#if NETCOREAPP3_0_OR_GREATER && !NET11_0_OR_GREATER

namespace Polyfills;

using System;
using System.Text;

static partial class Polyfill
{
    /// <summary>
    /// Returns a value indicating whether a specified rune occurs within this string using an ordinal comparison.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.string.contains?view=net-11.0#system-string-contains(system-text-rune)
    public static bool Contains(this string target, Rune value) =>
        value.IsBmp
            ? target.Contains((char) value.Value)
            : target.IndexOf(value, 0, target.Length, StringComparison.Ordinal) >= 0;

    /// <summary>
    /// Returns a value indicating whether a specified rune occurs within this string using the specified comparison option.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.string.contains?view=net-11.0#system-string-contains(system-text-rune-system-stringcomparison)
    public static bool Contains(this string target, Rune value, StringComparison comparisonType) =>
        target.IndexOf(value, 0, target.Length, comparisonType) >= 0;

    /// <summary>
    /// Determines whether the beginning of this string instance matches the specified rune using an ordinal comparison.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.string.startswith?view=net-11.0#system-string-startswith(system-text-rune)
    public static bool StartsWith(this string target, Rune value)
    {
        if (value.IsBmp)
        {
            return target.StartsWith((char) value.Value);
        }

        GetRuneSurrogates(value, out var high, out var low);
        return target.Length > 1 &&
               target[0] == high &&
               target[1] == low;
    }

    /// <summary>
    /// Determines whether the beginning of this string instance matches the specified rune when compared using the specified comparison option.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.string.startswith?view=net-11.0#system-string-startswith(system-text-rune-system-stringcomparison)
    public static bool StartsWith(this string target, Rune value, StringComparison comparisonType)
    {
        Span<char> buffer = stackalloc char[2];
        var chars = buffer.Slice(0, value.EncodeToUtf16(buffer));
        return target.AsSpan().StartsWith(chars, comparisonType);
    }

    /// <summary>
    /// Determines whether the end of this string instance matches the specified rune using an ordinal comparison.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.string.endswith?view=net-11.0#system-string-endswith(system-text-rune)
    public static bool EndsWith(this string target, Rune value)
    {
        if (value.IsBmp)
        {
            return target.EndsWith((char) value.Value);
        }

        GetRuneSurrogates(value, out var high, out var low);
        var length = target.Length;
        return length > 1 &&
               target[length - 2] == high &&
               target[length - 1] == low;
    }

    /// <summary>
    /// Determines whether the end of this string instance matches the specified rune when compared using the specified comparison option.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.string.endswith?view=net-11.0#system-string-endswith(system-text-rune-system-stringcomparison)
    public static bool EndsWith(this string target, Rune value, StringComparison comparisonType)
    {
        Span<char> buffer = stackalloc char[2];
        var chars = buffer.Slice(0, value.EncodeToUtf16(buffer));
        return target.AsSpan().EndsWith(chars, comparisonType);
    }

    /// <summary>
    /// Reports the zero-based index of the first occurrence of the specified rune in this string.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.string.indexof?view=net-11.0#system-string-indexof(system-text-rune)
    public static int IndexOf(this string target, Rune value) =>
        target.IndexOf(value, 0, target.Length, StringComparison.Ordinal);

    /// <summary>
    /// Reports the zero-based index of the first occurrence of the specified rune in this string.
    /// The search starts at a specified character position.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.string.indexof?view=net-11.0#system-string-indexof(system-text-rune-system-int32)
    public static int IndexOf(this string target, Rune value, int startIndex) =>
        target.IndexOf(value, startIndex, target.Length - startIndex, StringComparison.Ordinal);

    /// <summary>
    /// Reports the zero-based index of the first occurrence of the specified rune in this string.
    /// The search starts at a specified character position and examines a specified number of character positions.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.string.indexof?view=net-11.0#system-string-indexof(system-text-rune-system-int32-system-int32)
    public static int IndexOf(this string target, Rune value, int startIndex, int count) =>
        target.IndexOf(value, startIndex, count, StringComparison.Ordinal);

    /// <summary>
    /// Reports the zero-based index of the first occurrence of the specified rune in this string.
    /// A parameter specifies the type of search to use.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.string.indexof?view=net-11.0#system-string-indexof(system-text-rune-system-stringcomparison)
    public static int IndexOf(this string target, Rune value, StringComparison comparisonType) =>
        target.IndexOf(value, 0, target.Length, comparisonType);

    /// <summary>
    /// Reports the zero-based index of the first occurrence of the specified rune in this string.
    /// The search starts at a specified character position.
    /// A parameter specifies the type of search to use.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.string.indexof?view=net-11.0#system-string-indexof(system-text-rune-system-int32-system-stringcomparison)
    public static int IndexOf(this string target, Rune value, int startIndex, StringComparison comparisonType) =>
        target.IndexOf(value, startIndex, target.Length - startIndex, comparisonType);

    /// <summary>
    /// Reports the zero-based index of the first occurrence of the specified rune in this string.
    /// The search starts at a specified character position and examines a specified number of character positions.
    /// A parameter specifies the type of search to use.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.string.indexof?view=net-11.0#system-string-indexof(system-text-rune-system-int32-system-int32-system-stringcomparison)
    public static int IndexOf(this string target, Rune value, int startIndex, int count, StringComparison comparisonType)
    {
        if (startIndex < 0 ||
            startIndex > target.Length)
        {
            throw new ArgumentOutOfRangeException(nameof(startIndex));
        }

        if (count < 0 ||
            count > target.Length - startIndex)
        {
            throw new ArgumentOutOfRangeException(nameof(count));
        }

        Span<char> buffer = stackalloc char[2];
        var chars = buffer.Slice(0, value.EncodeToUtf16(buffer));
        var index = target.AsSpan(startIndex, count).IndexOf(chars, comparisonType);
        return index < 0 ? index : startIndex + index;
    }

    /// <summary>
    /// Reports the zero-based index of the last occurrence of the specified rune in this string.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.string.lastindexof?view=net-11.0#system-string-lastindexof(system-text-rune)
    public static int LastIndexOf(this string target, Rune value) =>
        target.LastIndexOf(value, target.Length - 1, target.Length, StringComparison.Ordinal);

    /// <summary>
    /// Reports the zero-based index of the last occurrence of the specified rune in this string.
    /// The search starts at a specified character position and proceeds backward toward the beginning of the string.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.string.lastindexof?view=net-11.0#system-string-lastindexof(system-text-rune-system-int32)
    public static int LastIndexOf(this string target, Rune value, int startIndex) =>
        target.LastIndexOf(value, startIndex, startIndex + 1, StringComparison.Ordinal);

    /// <summary>
    /// Reports the zero-based index of the last occurrence of the specified rune in this string.
    /// The search starts at a specified character position and proceeds backward toward the beginning of the string for a specified number of character positions.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.string.lastindexof?view=net-11.0#system-string-lastindexof(system-text-rune-system-int32-system-int32)
    public static int LastIndexOf(this string target, Rune value, int startIndex, int count) =>
        target.LastIndexOf(value, startIndex, count, StringComparison.Ordinal);

    /// <summary>
    /// Reports the zero-based index of the last occurrence of the specified rune in this string.
    /// A parameter specifies the type of search to use.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.string.lastindexof?view=net-11.0#system-string-lastindexof(system-text-rune-system-stringcomparison)
    public static int LastIndexOf(this string target, Rune value, StringComparison comparisonType) =>
        target.LastIndexOf(value, target.Length - 1, target.Length, comparisonType);

    /// <summary>
    /// Reports the zero-based index of the last occurrence of the specified rune in this string.
    /// The search starts at a specified character position and proceeds backward toward the beginning of the string.
    /// A parameter specifies the type of search to use.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.string.lastindexof?view=net-11.0#system-string-lastindexof(system-text-rune-system-int32-system-stringcomparison)
    public static int LastIndexOf(this string target, Rune value, int startIndex, StringComparison comparisonType) =>
        target.LastIndexOf(value, startIndex, startIndex + 1, comparisonType);

    /// <summary>
    /// Reports the zero-based index of the last occurrence of the specified rune in this string.
    /// The search starts at a specified character position and proceeds backward toward the beginning of the string for a specified number of character positions.
    /// A parameter specifies the type of search to use.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.string.lastindexof?view=net-11.0#system-string-lastindexof(system-text-rune-system-int32-system-int32-system-stringcomparison)
    public static int LastIndexOf(this string target, Rune value, int startIndex, int count, StringComparison comparisonType)
    {
        // as in the BCL, an empty string reports not found before any argument validation
        if (target.Length == 0)
        {
            return -1;
        }

        if (startIndex < 0 ||
            startIndex >= target.Length)
        {
            throw new ArgumentOutOfRangeException(nameof(startIndex));
        }

        if (count < 0 ||
            startIndex - count + 1 < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(count));
        }

        Span<char> buffer = stackalloc char[2];
        var chars = buffer.Slice(0, value.EncodeToUtf16(buffer));
        var startIndexFromZero = startIndex - count + 1;
        var index = target.AsSpan(startIndexFromZero, count).LastIndexOf(chars, comparisonType);
        return index < 0 ? index : startIndexFromZero + index;
    }

    /// <summary>
    /// Returns a new string in which all occurrences of a specified rune in this instance are replaced with another specified rune, using an ordinal comparison.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.string.replace?view=net-11.0#system-string-replace(system-text-rune-system-text-rune)
    public static string Replace(this string target, Rune oldRune, Rune newRune)
    {
        if (oldRune.IsBmp &&
            newRune.IsBmp)
        {
            return target.Replace((char) oldRune.Value, (char) newRune.Value);
        }

        return target.Replace(oldRune.ToString(), newRune.ToString());
    }

    /// <summary>
    /// Splits a string into substrings based on a specified delimiting rune and, optionally, options.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.string.split?view=net-11.0#system-string-split(system-text-rune-system-stringsplitoptions)
    public static string[] Split(this string target, Rune separator, StringSplitOptions options = StringSplitOptions.None) =>
        target.Split(separator, int.MaxValue, options);

    /// <summary>
    /// Splits a string into a maximum number of substrings based on the provided rune separator, optionally omitting empty substrings from the result.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.string.split?view=net-11.0#system-string-split(system-text-rune-system-int32-system-stringsplitoptions)
    public static string[] Split(this string target, Rune separator, int count, StringSplitOptions options = StringSplitOptions.None)
    {
        if (separator.IsBmp)
        {
            return target.Split((char) separator.Value, count, options);
        }

        // the BCL matches the string separator overload for supplementary-plane separators
        return target.Split(separator.ToString(), count, options);
    }

    /// <summary>
    /// Removes all leading and trailing instances of a rune from the current string.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.string.trim?view=net-11.0#system-string-trim(system-text-rune)
    public static string Trim(this string target, Rune trimRune)
    {
        if (trimRune.IsBmp)
        {
            return target.Trim((char) trimRune.Value);
        }

        GetRuneSurrogates(trimRune, out var high, out var low);

        var index = 0;
        while (index + 1 < target.Length &&
               target[index] == high &&
               target[index + 1] == low)
        {
            index += 2;
        }

        var endIndex = target.Length - 2;
        while (endIndex > index &&
               target[endIndex] == high &&
               target[endIndex + 1] == low)
        {
            endIndex -= 2;
        }

        return target.Substring(index, endIndex + 2 - index);
    }

    /// <summary>
    /// Removes all leading instances of a rune from the current string.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.string.trimstart?view=net-11.0#system-string-trimstart(system-text-rune)
    public static string TrimStart(this string target, Rune trimRune)
    {
        if (trimRune.IsBmp)
        {
            return target.TrimStart((char) trimRune.Value);
        }

        GetRuneSurrogates(trimRune, out var high, out var low);

        var index = 0;
        while (index + 1 < target.Length &&
               target[index] == high &&
               target[index + 1] == low)
        {
            index += 2;
        }

        return target.Substring(index);
    }

    /// <summary>
    /// Removes all trailing instances of a rune from the current string.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.string.trimend?view=net-11.0#system-string-trimend(system-text-rune)
    public static string TrimEnd(this string target, Rune trimRune)
    {
        if (trimRune.IsBmp)
        {
            return target.TrimEnd((char) trimRune.Value);
        }

        GetRuneSurrogates(trimRune, out var high, out var low);

        var endIndex = target.Length - 2;
        while (endIndex >= 0 &&
               target[endIndex] == high &&
               target[endIndex + 1] == low)
        {
            endIndex -= 2;
        }

        return target.Substring(0, endIndex + 2);
    }

    static void GetRuneSurrogates(Rune value, out char high, out char low)
    {
        var offset = value.Value - 0x10000;
        high = (char) (0xD800 + (offset >> 10));
        low = (char) (0xDC00 + (offset & 0x3FF));
    }
}

#endif
