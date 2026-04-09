#if FeatureMemory && !NET8_0_OR_GREATER

namespace Polyfills;

using System;
using System.Collections.Generic;

static partial class Polyfill
{
    // StringSplitOptions.TrimEntries was added in .NET 5; reference by value so this
    // file also compiles on net461/netstandard2.0 where the named member is missing.
    const StringSplitOptions TrimEntries = (StringSplitOptions)2;
    const StringSplitOptions AllSplitOptions = StringSplitOptions.RemoveEmptyEntries | TrimEntries;

    /// <summary>
    /// Parses the source <see cref="ReadOnlySpan{Char}"/> for the specified <paramref name="separator"/>, populating
    /// the <paramref name="destination"/> span with <see cref="Range"/> instances representing the regions between the
    /// separators.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.split?view=net-11.0#system-memoryextensions-split(system-readonlyspan((system-char))-system-span((system-range))-system-char-system-stringsplitoptions)
    public static int Split(this ReadOnlySpan<char> source, Span<Range> destination, char separator, StringSplitOptions options = StringSplitOptions.None)
    {
        CheckStringSplitOptions(options);

        var indices = new List<int>();
        for (var i = 0; i < source.Length; i++)
        {
            if (source[i] == separator)
            {
                indices.Add(i);
            }
        }

        return SplitRangesCore(source, destination, indices, null, 1, options);
    }

    /// <summary>
    /// Parses the source <see cref="ReadOnlySpan{Char}"/> for the specified <paramref name="separator"/>, populating
    /// the <paramref name="destination"/> span with <see cref="Range"/> instances representing the regions between the
    /// separators.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.split?view=net-11.0#system-memoryextensions-split(system-readonlyspan((system-char))-system-span((system-range))-system-readonlyspan((system-char))-system-stringsplitoptions)
    public static int Split(this ReadOnlySpan<char> source, Span<Range> destination, ReadOnlySpan<char> separator, StringSplitOptions options = StringSplitOptions.None)
    {
        CheckStringSplitOptions(options);

        // If the separator is empty, the whole input is the sole range, subject to the options.
        if (separator.IsEmpty)
        {
            if (!destination.IsEmpty)
            {
                var startInclusive = 0;
                var endExclusive = source.Length;

                if ((options & TrimEntries) != 0)
                {
                    TrimSplitEntry(source, ref startInclusive, ref endExclusive);
                }

                if (startInclusive != endExclusive || (options & StringSplitOptions.RemoveEmptyEntries) == 0)
                {
                    destination[0] = startInclusive..endExclusive;
                    return 1;
                }
            }

            return 0;
        }

        var indices = new List<int>();
        var i = 0;
        while (i <= source.Length - separator.Length)
        {
            var found = source.Slice(i).IndexOf(separator);
            if (found < 0)
            {
                break;
            }

            indices.Add(i + found);
            i += found + separator.Length;
        }

        return SplitRangesCore(source, destination, indices, null, separator.Length, options);
    }

    /// <summary>
    /// Parses the source <see cref="ReadOnlySpan{Char}"/> for one of the specified <paramref name="separators"/>,
    /// populating the <paramref name="destination"/> span with <see cref="Range"/> instances representing the regions
    /// between the separators.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.splitany?view=net-11.0#system-memoryextensions-splitany(system-readonlyspan((system-char))-system-span((system-range))-system-readonlyspan((system-char))-system-stringsplitoptions)
    public static int SplitAny(this ReadOnlySpan<char> source, Span<Range> destination, ReadOnlySpan<char> separators, StringSplitOptions options = StringSplitOptions.None)
    {
        CheckStringSplitOptions(options);

        // If the separators list is empty, whitespace is used as separators. In that case ignore TrimEntries
        // (since TrimEntries also impacts whitespace) — but only if the destination is large enough that we
        // are not constrained by count, since when constrained the last substring still needs to be processed.
        var useWhitespace = separators.IsEmpty;
        if (useWhitespace && destination.Length > source.Length)
        {
            options &= ~TrimEntries;
        }

        var indices = new List<int>();
        for (var i = 0; i < source.Length; i++)
        {
            var c = source[i];
            var isSeparator = useWhitespace ? char.IsWhiteSpace(c) : separators.IndexOf(c) >= 0;
            if (isSeparator)
            {
                indices.Add(i);
            }
        }

        return SplitRangesCore(source, destination, indices, null, 1, options);
    }

    /// <summary>
    /// Parses the source <see cref="ReadOnlySpan{Char}"/> for one of the specified <paramref name="separators"/>,
    /// populating the <paramref name="destination"/> span with <see cref="Range"/> instances representing the regions
    /// between the separators.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.splitany?view=net-11.0#system-memoryextensions-splitany(system-readonlyspan((system-char))-system-span((system-range))-system-readonlyspan((system-string))-system-stringsplitoptions)
    public static int SplitAny(this ReadOnlySpan<char> source, Span<Range> destination, ReadOnlySpan<string> separators, StringSplitOptions options = StringSplitOptions.None)
    {
        CheckStringSplitOptions(options);

        var useWhitespace = separators.IsEmpty;
        if (useWhitespace && destination.Length > source.Length)
        {
            options &= ~TrimEntries;
        }

        var indices = new List<int>();
        List<int>? lengths;

        if (useWhitespace)
        {
            lengths = null;
            for (var i = 0; i < source.Length; i++)
            {
                if (char.IsWhiteSpace(source[i]))
                {
                    indices.Add(i);
                }
            }

            return SplitRangesCore(source, destination, indices, null, 1, options);
        }

        lengths = new();
        var pos = 0;
        while (pos < source.Length)
        {
            var matchLength = 0;
            for (var s = 0; s < separators.Length; s++)
            {
                var sep = separators[s];
                if (string.IsNullOrEmpty(sep))
                {
                    continue;
                }

                if (pos + sep.Length > source.Length)
                {
                    continue;
                }

                if (source.Slice(pos, sep.Length).SequenceEqual(sep.AsSpan()))
                {
                    matchLength = sep.Length;
                    break;
                }
            }

            if (matchLength > 0)
            {
                indices.Add(pos);
                lengths.Add(matchLength);
                pos += matchLength;
            }
            else
            {
                pos++;
            }
        }

        return SplitRangesCore(source, destination, indices, lengths, -1, options);
    }

    /// <summary>Core implementation that mirrors the BCL SplitCore behaviour.</summary>
    static int SplitRangesCore(
        ReadOnlySpan<char> source,
        Span<Range> destination,
        List<int> separatorIndices,
        List<int>? separatorLengths,
        int defaultSeparatorLength,
        StringSplitOptions options)
    {
        if (destination.IsEmpty)
        {
            return 0;
        }

        var keepEmptyEntries = (options & StringSplitOptions.RemoveEmptyEntries) == 0;
        var trimEntries = (options & TrimEntries) != 0;

        if (source.Length == 0)
        {
            if (keepEmptyEntries)
            {
                // Write 0..0 explicitly rather than default(Range): on TFMs where Range is
                // polyfilled as a reference-type record, default(Range) would be null.
                destination[0] = 0..0;
                return 1;
            }

            return 0;
        }

        var startInclusive = 0;
        int endExclusive;

        if (destination.Length == 1)
        {
            endExclusive = source.Length;
            if (trimEntries)
            {
                TrimSplitEntry(source, ref startInclusive, ref endExclusive);
            }

            if (startInclusive != endExclusive || keepEmptyEntries)
            {
                destination[0] = startInclusive..endExclusive;
                return 1;
            }

            return 0;
        }

        var rangeCount = 0;
        var separatorIndex = 0;
        var destinationMinusOne = destination.Slice(0, destination.Length - 1);
        var separatorLength = defaultSeparatorLength;

        while (separatorIndex < separatorIndices.Count &&
               (rangeCount < destinationMinusOne.Length || !keepEmptyEntries))
        {
            endExclusive = separatorIndices[separatorIndex];
            if (separatorLengths != null)
            {
                separatorLength = separatorLengths[separatorIndex];
            }

            separatorIndex++;

            var untrimmedEndExclusive = endExclusive;
            if (trimEntries)
            {
                TrimSplitEntry(source, ref startInclusive, ref endExclusive);
            }

            if (startInclusive != endExclusive || keepEmptyEntries)
            {
                if ((uint)rangeCount >= (uint)destinationMinusOne.Length)
                {
                    break;
                }

                destinationMinusOne[rangeCount] = startInclusive..endExclusive;
                rangeCount++;
            }

            startInclusive = untrimmedEndExclusive + separatorLength;
        }

        // The last destination slot (if available and not yet filled) gets the remainder of the source.
        if ((uint)rangeCount < (uint)destination.Length)
        {
            endExclusive = source.Length;
            if (trimEntries)
            {
                TrimSplitEntry(source, ref startInclusive, ref endExclusive);
            }

            if (startInclusive != endExclusive || keepEmptyEntries)
            {
                destination[rangeCount] = startInclusive..endExclusive;
                rangeCount++;
            }
        }

        return rangeCount;
    }

    static void TrimSplitEntry(ReadOnlySpan<char> source, ref int startInclusive, ref int endExclusive)
    {
        while (startInclusive < endExclusive && char.IsWhiteSpace(source[startInclusive]))
        {
            startInclusive++;
        }

        while (endExclusive > startInclusive && char.IsWhiteSpace(source[endExclusive - 1]))
        {
            endExclusive--;
        }
    }

    static void CheckStringSplitOptions(StringSplitOptions options)
    {
        if ((options & ~AllSplitOptions) != 0)
        {
            throw new ArgumentException($"Illegal enum value: {(int)options}", nameof(options));
        }
    }
}

#endif
