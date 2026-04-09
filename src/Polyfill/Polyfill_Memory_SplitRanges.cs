#if FeatureMemory && !NET8_0_OR_GREATER

namespace Polyfills;

using System;

static partial class Polyfill
{
    // StringSplitOptions.TrimEntries was added in .NET 5; reference by value so this
    // file also compiles on net461/netstandard2.0 where the named member is missing.
    const StringSplitOptions TrimEntriesFlag = (StringSplitOptions)2;
    const StringSplitOptions ValidSplitOptions = StringSplitOptions.RemoveEmptyEntries | TrimEntriesFlag;

    enum SplitRangesMode
    {
        SingleChar,
        Sequence,
        EmptySequence,
        AnyChars,
        AnyStrings,
        AnyWhitespace,
    }

    /// <summary>
    /// Parses the source <see cref="ReadOnlySpan{Char}"/> for the specified <paramref name="separator"/>, populating
    /// the <paramref name="destination"/> span with <see cref="Range"/> instances representing the regions between the
    /// separators.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.split?view=net-11.0#system-memoryextensions-split(system-readonlyspan((system-char))-system-span((system-range))-system-char-system-stringsplitoptions)
    public static int Split(this ReadOnlySpan<char> source, Span<Range> destination, char separator, StringSplitOptions options = StringSplitOptions.None)
    {
        CheckSplitOptions(options);
        return SplitRangesCore(source, destination, separator, default, default, SplitRangesMode.SingleChar, options);
    }

    /// <summary>
    /// Parses the source <see cref="ReadOnlySpan{Char}"/> for the specified <paramref name="separator"/>, populating
    /// the <paramref name="destination"/> span with <see cref="Range"/> instances representing the regions between the
    /// separators.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.split?view=net-11.0#system-memoryextensions-split(system-readonlyspan((system-char))-system-span((system-range))-system-readonlyspan((system-char))-system-stringsplitoptions)
    public static int Split(this ReadOnlySpan<char> source, Span<Range> destination, ReadOnlySpan<char> separator, StringSplitOptions options = StringSplitOptions.None)
    {
        CheckSplitOptions(options);
        var mode = separator.IsEmpty ? SplitRangesMode.EmptySequence : SplitRangesMode.Sequence;
        return SplitRangesCore(source, destination, default, separator, default, mode, options);
    }

    /// <summary>
    /// Parses the source <see cref="ReadOnlySpan{Char}"/> for one of the specified <paramref name="separators"/>,
    /// populating the <paramref name="destination"/> span with <see cref="Range"/> instances representing the regions
    /// between the separators.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.splitany?view=net-11.0#system-memoryextensions-splitany(system-readonlyspan((system-char))-system-span((system-range))-system-readonlyspan((system-char))-system-stringsplitoptions)
    public static int SplitAny(this ReadOnlySpan<char> source, Span<Range> destination, ReadOnlySpan<char> separators, StringSplitOptions options = StringSplitOptions.None)
    {
        CheckSplitOptions(options);
        var mode = SplitRangesMode.AnyChars;
        if (separators.IsEmpty)
        {
            mode = SplitRangesMode.AnyWhitespace;
            // Whitespace separators interfere with TrimEntries; the BCL discards Trim unless the count
            // constraint forces the last substring to be processed.
            if (destination.Length > source.Length)
            {
                options &= ~TrimEntriesFlag;
            }
        }

        return SplitRangesCore(source, destination, default, separators, default, mode, options);
    }

    /// <summary>
    /// Parses the source <see cref="ReadOnlySpan{Char}"/> for one of the specified <paramref name="separators"/>,
    /// populating the <paramref name="destination"/> span with <see cref="Range"/> instances representing the regions
    /// between the separators.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.splitany?view=net-11.0#system-memoryextensions-splitany(system-readonlyspan((system-char))-system-span((system-range))-system-readonlyspan((system-string))-system-stringsplitoptions)
    public static int SplitAny(this ReadOnlySpan<char> source, Span<Range> destination, ReadOnlySpan<string> separators, StringSplitOptions options = StringSplitOptions.None)
    {
        CheckSplitOptions(options);
        var mode = SplitRangesMode.AnyStrings;
        if (separators.IsEmpty)
        {
            mode = SplitRangesMode.AnyWhitespace;
            if (destination.Length > source.Length)
            {
                options &= ~TrimEntriesFlag;
            }
        }

        return SplitRangesCore(source, destination, default, default, separators, mode, options);
    }

    /// <summary>
    /// Unified core that mirrors the BCL <c>SplitCore</c> behaviour. Finds separators lazily via
    /// <see cref="FindNextSeparator"/> rather than precomputing them, so each public method needs only a thin shim.
    /// </summary>
    static int SplitRangesCore(
        ReadOnlySpan<char> source,
        Span<Range> destination,
        char singleChar,
        ReadOnlySpan<char> charSeparators,
        ReadOnlySpan<string> stringSeparators,
        SplitRangesMode mode,
        StringSplitOptions options)
    {
        if (destination.IsEmpty)
        {
            return 0;
        }

        var keepEmpty = (options & StringSplitOptions.RemoveEmptyEntries) == 0;
        var trim = (options & TrimEntriesFlag) != 0;

        if (source.Length == 0)
        {
            if (keepEmpty)
            {
                // Write 0..0 explicitly: on TFMs where Range is polyfilled as a record, default(Range) would be null.
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
            if (trim)
            {
                TrimSplitEntry(source, ref startInclusive, ref endExclusive);
            }

            if (startInclusive != endExclusive || keepEmpty)
            {
                destination[0] = startInclusive..endExclusive;
                return 1;
            }

            return 0;
        }

        var rangeCount = 0;
        var destinationMinusOne = destination.Slice(0, destination.Length - 1);

        // Loop while we still have room AND (slots remaining OR we are skipping empties to find the start of the
        // remainder slot). The second condition matches the BCL: with RemoveEmptyEntries, we keep iterating past
        // empty entries even after destinationMinusOne is full so the final slot anchors at the next non-empty.
        while (rangeCount < destinationMinusOne.Length || !keepEmpty)
        {
            if (!FindNextSeparator(source, startInclusive, mode, singleChar, charSeparators, stringSeparators, out endExclusive, out var separatorLength))
            {
                break;
            }

            var untrimmedEndExclusive = endExclusive;
            if (trim)
            {
                TrimSplitEntry(source, ref startInclusive, ref endExclusive);
            }

            if (startInclusive != endExclusive || keepEmpty)
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
            if (trim)
            {
                TrimSplitEntry(source, ref startInclusive, ref endExclusive);
            }

            if (startInclusive != endExclusive || keepEmpty)
            {
                destination[rangeCount] = startInclusive..endExclusive;
                rangeCount++;
            }
        }

        return rangeCount;
    }

    /// <summary>
    /// Finds the next separator at or after <paramref name="from"/>, dispatching on <paramref name="mode"/>.
    /// Defers to BCL <c>IndexOf</c>/<c>IndexOfAny</c> wherever possible so the per-mode logic stays minimal.
    /// </summary>
    static bool FindNextSeparator(
        ReadOnlySpan<char> source,
        int from,
        SplitRangesMode mode,
        char singleChar,
        ReadOnlySpan<char> charSeparators,
        ReadOnlySpan<string> stringSeparators,
        out int separatorIndex,
        out int separatorLength)
    {
        var idx = -1;
        var len = 1;

        if (from <= source.Length)
        {
            var slice = source.Slice(from);
            switch (mode)
            {
                case SplitRangesMode.SingleChar:
                    idx = slice.IndexOf(singleChar);
                    break;

                case SplitRangesMode.Sequence:
                    idx = slice.IndexOf(charSeparators);
                    len = charSeparators.Length;
                    break;

                case SplitRangesMode.AnyChars:
                    idx = slice.IndexOfAny(charSeparators);
                    break;

                case SplitRangesMode.AnyWhitespace:
                    for (var i = 0; i < slice.Length; i++)
                    {
                        if (char.IsWhiteSpace(slice[i]))
                        {
                            idx = i;
                            break;
                        }
                    }

                    break;

                case SplitRangesMode.AnyStrings:
                    // Per-separator BCL IndexOf, then keep the leftmost match. Ties resolve by array order
                    // (first separator wins) — matching the BCL's MakeSeparatorListAny behaviour.
                    for (var s = 0; s < stringSeparators.Length; s++)
                    {
                        var sep = stringSeparators[s];
                        if (string.IsNullOrEmpty(sep))
                        {
                            continue;
                        }

                        var found = slice.IndexOf(sep.AsSpan());
                        if (found >= 0 && (idx < 0 || found < idx))
                        {
                            idx = found;
                            len = sep.Length;
                        }
                    }

                    break;

                // SplitRangesMode.EmptySequence: idx stays -1, so the core's "no separators found" path
                // writes the whole input as the sole range.
            }
        }

        if (idx < 0)
        {
            separatorIndex = -1;
            separatorLength = 0;
            return false;
        }

        separatorIndex = from + idx;
        separatorLength = len;
        return true;
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

    static void CheckSplitOptions(StringSplitOptions options)
    {
        if ((options & ~ValidSplitOptions) != 0)
        {
            throw new ArgumentException($"Illegal enum value: {(int)options}", nameof(options));
        }
    }
}

#endif
