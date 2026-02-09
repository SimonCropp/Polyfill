namespace Polyfills;

using System;
using System.Text;
using System.IO;
using System.Runtime.CompilerServices;

static partial class Polyfill
{
#if FeatureMemory && !NET6_0_OR_GREATER

    /// <summary>
    /// Copies the contents of this string into the destination span.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.string.copyto?view=net-10.0
    public static void CopyTo(this string target, Span<char> destination) =>
        target.AsSpan().CopyTo(destination);

    /// <summary>
    /// Copies the contents of this string into the destination span.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.string.trycopyto?view=net-10.0
    public static bool TryCopyTo(this string target, Span<char> destination) =>
        target.AsSpan().TryCopyTo(destination);
#endif

#if NETFRAMEWORK || NETSTANDARD2_0

    /// <summary>
    /// Returns the hash code for this string using the specified rules.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.string.gethashcode?view=net-10.0#system-string-gethashcode(system-stringcomparison)
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
            _ => throw new ArgumentOutOfRangeException(nameof(comparison), comparison, null)
        };

    /// <summary>
    /// Returns a value indicating whether a specified character occurs within this string, using the specified comparison rules.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.string.contains?view=net-10.0#system-string-contains(system-char-system-stringcomparison)
    public static bool Contains(this string target, char value, StringComparison comparisonType) =>
        target.IndexOf(value, comparisonType) >= 0;

    /// <summary>
    /// Reports the zero-based index of the first occurrence of the specified Unicode character in this string. A parameter specifies the type of search to use for the specified character.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.string.indexof?view=net-10.0#system-string-indexof(system-char-system-stringcomparison)
    public static int IndexOf(this string target, char value, StringComparison comparisonType) =>
        target.IndexOf(value.ToString(), comparisonType);

    /// <summary>
    /// Returns a value indicating whether a specified string occurs within this string, using the specified comparison rules.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.string.contains?view=net-10.0#system-string-contains(system-string-system-stringcomparison)
    public static bool Contains(this string target, string value, StringComparison comparisonType) =>
        target.IndexOf(value, comparisonType) >= 0;

    /// <summary>
    /// Determines whether this string instance starts with the specified character.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.string.startswith?view=net-10.0#system-string-startswith(system-char)
    public static bool StartsWith(this string target, char value)
    {
        if (target.Length == 0)
        {
            return false;
        }

        return target[0] == value;
    }

    /// <summary>
    /// Returns a value indicating whether a specified character occurs within this string.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.string.endswith?view=net-10.0#system-string-endswith(system-char)
    public static bool EndsWith(this string target, char value)
    {
        if (target.Length == 0)
        {
            return false;
        }

        var lastPos = target.Length - 1;
        return target[lastPos] == value;
    }

    /// <summary>
    /// Splits a string into substrings based on a specified delimiting character and, optionally, options.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.string.split?view=net-10.0#system-string-split(system-char-system-stringsplitoptions)
    public static string[] Split(this string target, char separator, StringSplitOptions options = StringSplitOptions.None) =>
        target.Split([separator], options);

    /// <summary>
    /// Splits a string into a maximum number of substrings based on a specified delimiting character and, optionally,
    /// options. Splits a string into a maximum number of substrings based on the provided character separator,
    /// optionally omitting empty substrings from the result.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.string.split?view=net-10.0#system-string-split(system-char-system-int32-system-stringsplitoptions)
    public static string[] Split(this string target, char separator, int count, StringSplitOptions options = StringSplitOptions.None) =>
        target.Split([separator], count, options);

    /// <summary>
    /// Splits a string into substrings that are based on the provided string separator.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.string.split?view=net-10.0#system-string-split(system-string-system-stringsplitoptions)
    public static string[] Split(this string target, string separator, StringSplitOptions options = StringSplitOptions.None) =>
        target.Split([separator], options);

    /// <summary>
    /// Splits a string into a maximum number of substrings based on a specified delimiting string and, optionally, options.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.string.split?view=net-10.0#system-string-split(system-string-system-int32-system-stringsplitoptions)
    public static string[] Split(this string target, string separator, int count, StringSplitOptions options = StringSplitOptions.None) =>
        target.Split([separator], count, options);
#endif

#if !NETCOREAPP2_1_OR_GREATER && !NETSTANDARD2_1_OR_GREATER
    /// <summary>
    /// Returns a value indicating whether a specified character occurs within this string.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.string.contains?view=net-10.0#system-string-contains(system-char)
    public static bool Contains(this string target, char value) =>
        target.IndexOf(value) >= 0;
#endif

#if !NET6_0_OR_GREATER
    /// <summary>
    /// Replaces all newline sequences in the current string with <paramref name="replacementText"/>.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.string.replacelineendings?view=net-10.0#system-string-replacelineendings(system-string)
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string ReplaceLineEndings(this string target, string replacementText)
    {
        var builder = new StringBuilder(target.Length);
        using var reader = new StringReader(target);
        while (true)
        {
            var line = reader.ReadLine();
            if (line == null)
            {
                break;
            }

            builder.Append(line);
            builder.Append(replacementText);
        }

        return builder.ToString(0, builder.Length - replacementText.Length);
    }

    /// <summary>
    /// Replaces all newline sequences in the current string with <see cref="Environment.NewLine"/>.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.string.replacelineendings?view=net-10.0#system-string-replacelineendings
    public static string ReplaceLineEndings(this string target) =>
        ReplaceLineEndings(target, Environment.NewLine);
#endif
}